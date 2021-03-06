﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sys = System;
public class Maze : MonoBehaviour
{

    (int width, int height) size = (31, 31);
    (int x, int y) origin = (0, 0);
    GameObject[,] mazescape;

    public GameObject player;
    public GameObject cellPrefab;
    public GameObject spawnpointPrefab;
    public GameObject enemy;
    public GameObject generatorPrefab;

    public int maxTries;
    public int minRooms;
    public List<Room> rooms;

    public GameObject gameController;
    public GameObject gate;

    // Start is called before the first frame update
    void Start()
    {
        cellPrefab.GetComponent<Cell>().size = 32;

        maxTries = 30;
        minRooms = 3;
        rooms = new List<Room>();

        generateNewMaze();

        makeRooms();
        //makeTestRooms();

        makeHallways();

        makeExit();

        gameController.GetComponent<GameController>().generatorsOn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generateNewMaze()
    {
        mazescape = new GameObject[size.height, size.width];

        for (int i = 0; i < mazescape.GetLength(0); i++)
        {
            for (int j = 0; j < mazescape.GetLength(1); j++)
            {
                mazescape[i, j] = Instantiate(
                    cellPrefab,
                    new Vector3(origin.x + (j * cellPrefab.GetComponent<Cell>().size) / 100, origin.y - (i * cellPrefab.GetComponent<Cell>().size) / 100, 0),
                    Quaternion.identity,
                    this.transform
                    );
            }
        }
    }

    void makeRooms()
    {
        // Generate random room (until maxTries)
        for (int k = 0; k < maxTries; k++)
        {
            (int width, int height) roomSize = (Random.Range(2, 8), Random.Range(2, 8));
            (int x, int y) position = (Random.Range(1, size.width - roomSize.width), Random.Range(1, size.height - roomSize.height));
            Room newRoom = new Room(mazescape[position.y, position.x], position, roomSize);

            // Check for collisions
            if (!roomCollision(newRoom))
            {
                rooms.Add(newRoom);

                // Hollow out new room
                for (int i = newRoom.position.y; i < newRoom.size.height + newRoom.position.y - 1; i++)
                {
                    for (int j = newRoom.position.x; j < newRoom.size.width + newRoom.position.x - 1; j++)
                    {
                        mazescape[i, j].GetComponent<Cell>().open();

                        // Random chance to instantiate a spawnpoint on the cell
                        if (Random.Range(0, 21) == 20)
                        {
                            Instantiate(spawnpointPrefab,
                                        mazescape[i, j].transform.position,
                                        Quaternion.identity,
                                        mazescape[i, j].transform);
                        }
                    }
                }

                // Spawn generator in room
                Instantiate(generatorPrefab,
                            mazescape[rooms[rooms.Count - 1].position.y + Random.Range(0, rooms[rooms.Count - 1].size.height - 1), rooms[rooms.Count - 1].position.x + Random.Range(0, rooms[rooms.Count - 1].size.width - 1)].transform.position,
                            Quaternion.identity);
                gameController.GetComponent<GameController>().generators++;

                // Spawn player in first room
                if(k == 0){
                    player.transform.position = mazescape[rooms[0].position.y + Random.Range(0, rooms[0].size.height), rooms[0].position.x + Random.Range(0, rooms[0].size.width)].transform.position;
                }

                // Spawn enemy in second room
                if(rooms.Count == 2){
                    enemy.transform.position = mazescape[rooms[1].position.y + Random.Range(0, rooms[1].size.height), rooms[1].position.x + Random.Range(0, rooms[1].size.width)].transform.position;
                }
            }
        }
    }

    void makeTestRooms()
    {
        rooms.Add(new Room(mazescape[16, 20], (16, 20), (6, 5)));
        rooms.Add(new Room(mazescape[6, 4], (6, 4), (4, 4)));
        rooms.Add(new Room(mazescape[10, 10], (10, 10), (3, 7)));

        for (int i = 20; i < 25; i++)
        {
            for (int j = 16; j < 22; j++)
            {
                mazescape[i, j].GetComponent<Cell>().open();
            }
        }

        for (int i = 4; i < 8; i++)
        {
            for (int j = 6; j < 10; j++)
            {
                mazescape[i, j].GetComponent<Cell>().open();
            }
        }

        for (int i = 10; i < 17; i++)
        {
            for (int j = 10; j < 13; j++)
            {
                mazescape[i, j].GetComponent<Cell>().open();
            }
        }
    }

    bool roomCollision(Room newRoom)
    {
        foreach (Room room in rooms)
        {
            if (newRoom.position.x + newRoom.size.width > room.position.x &&
            newRoom.position.x < room.position.x + room.size.width &&
            newRoom.position.y + newRoom.size.height > room.position.y &&
            newRoom.position.y < room.position.y + room.size.height)
            {
                //Debug.Log("Collision!");
                return true;
            }
        }
        //Debug.Log("New room found...");
        return false;
    }

    public class Room
    {
        public GameObject originCell;
        public (int x, int y) position;
        public (int width, int height) size;

        public Room(GameObject originCell, (int x, int y) position, (int width, int height) size)
        {
            this.originCell = originCell;
            this.position = position;
            this.size = size;
        }
    }

    void makeHallways()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            // Choose another room at random
            int j = Random.Range(0, rooms.Count - 1);
            while (j == i)
            {
                j = Random.Range(0, rooms.Count - 1);
            }

            // Choose a random column to carve towards
            int xHallway = Random.Range(2, size.width - 3);

            // Choose a random wall of room i to open into a corridor
            (int x, int y) iyDoor = ((Sys.Math.Abs(rooms[i].position.x - xHallway) < Sys.Math.Abs(rooms[i].position.x + rooms[i].size.width - 1 - xHallway)) ? rooms[i].position.x : rooms[i].position.x + rooms[i].size.width - 1,
                                        Random.Range(rooms[i].position.y, rooms[i].position.y + rooms[i].size.height - 1));
            if (iyDoor.x < xHallway)
            {
                for (int corridor = iyDoor.x; corridor < xHallway; corridor++)
                {
                    //mazescape[iyDoor.y - 1, corridor].GetComponent<Cell>().open();
                    mazescape[iyDoor.y, corridor].GetComponent<Cell>().open();
                    //mazescape[iyDoor.y + 1, corridor].GetComponent<Cell>().open();
                }
            }
            else
            {
                for (int corridor = xHallway; corridor < iyDoor.x; corridor++)
                {
                    // mazescape[iyDoor.y - 1, corridor].GetComponent<Cell>().open();
                    mazescape[iyDoor.y, corridor].GetComponent<Cell>().open();
                    //mazescape[iyDoor.y + 1, corridor].GetComponent<Cell>().open();
                }
            }

            // Choose a random wall of room j to open into a corridor
            (int x, int y) jyDoor = ((Sys.Math.Abs(rooms[j].position.x - xHallway) < Sys.Math.Abs(rooms[j].position.x + rooms[j].size.width - 1 - xHallway)) ? rooms[j].position.x : rooms[j].position.x + rooms[j].size.width - 1,
                                        Random.Range(rooms[j].position.y, rooms[j].position.y + rooms[j].size.height - 1));
            if (jyDoor.x < xHallway)
            {
                for (int corridor = jyDoor.x; corridor < xHallway; corridor++)
                {
                    //mazescape[jyDoor.y - 1, corridor].GetComponent<Cell>().open();
                    mazescape[jyDoor.y, corridor].GetComponent<Cell>().open();
                    //mazescape[jyDoor.y + 1, corridor].GetComponent<Cell>().open();
                }
            }
            else
            {
                for (int corridor = xHallway; corridor < jyDoor.x; corridor++)
                {
                    //mazescape[jyDoor.y - 1, corridor].GetComponent<Cell>().open();
                    mazescape[jyDoor.y, corridor].GetComponent<Cell>().open();
                    //mazescape[jyDoor.y + 1, corridor].GetComponent<Cell>().open();
                }
            }

            // Carve main hallway
            int start = (iyDoor.y < jyDoor.y) ? iyDoor.y : jyDoor.y;
            int end = (iyDoor.y > jyDoor.y) ? iyDoor.y : jyDoor.y;
            for (int k = start; k <= end; k++)
            {
                mazescape[k, xHallway - 1].GetComponent<Cell>().open();
                mazescape[k, xHallway].GetComponent<Cell>().open();
                mazescape[k, xHallway + 1].GetComponent<Cell>().open();
            }

            int y = Random.Range(1, size.width - 3);
        }
    }

    void makeExit()
    {
        // Choose exit (on north or south)
        (int x, int y) exit = (0, 0);
        exit.x = Random.Range(1, size.width - 2);

        // Choose random room to connect to
        Room room = rooms[Random.Range(0, rooms.Count - 1)];
        (int x, int y) door = (Random.Range(room.position.x, room.position.x + room.size.width - 1),
                                Random.Range(room.position.y, room.position.y + room.size.height - 1));

        // Carve exit hallway
        if (exit.y > door.y)
        {
            for (int i = door.y; i < exit.y; i++)
            {
                mazescape[i, exit.x].GetComponent<Cell>().open();
            }
        }
        else
        {
            for (int i = exit.y; i < door.y; i++)
            {
                mazescape[i, exit.x].GetComponent<Cell>().open();
            }
        }

        // Carve room corridor
        if (exit.x > door.x)
        {
            for (int i = door.x; i < exit.x + 1; i++)
            {
                mazescape[door.y, i].GetComponent<Cell>().open();
            }
        }
        else
        {
            for (int i = exit.x; i < door.x + 1; i++)
            {
                mazescape[door.y, i].GetComponent<Cell>().open();
            }
        }

        gate.transform.position = mazescape[exit.y, exit.x].transform.position;
    }
}
