using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Maze : MonoBehaviour
{

    (int width, int height) size = (5, 5);
    (int x, int y) origin = (0, 0);
    GameObject[,] mazescape;
    
    public GameObject cellPrefab;

    public int maxTries;
    public int numRooms;

    // Start is called before the first frame update
    void Start()
    {
        cellPrefab.GetComponent<Cell>().size = 32;

        maxTries = 30;
        numRooms = 0;
        
        // Generate new closed maze
        mazescape = new GameObject[size.height, size.width];

        for(int i = 0; i < mazescape.GetLength(0); i++){
            for(int j = 0; j < mazescape.GetLength(1); j++){
                mazescape[i,j] = Instantiate(
                    cellPrefab, 
                    new Vector3(origin.x + (j * cellPrefab.GetComponent<Cell>().size) / 100, origin.y + (i * cellPrefab.GetComponent<Cell>().size) / 100, 0), 
                    Quaternion.identity,
                    this.transform
                    );
            }
        }

        // Generate rooms
        /*for(int i = 0; i < numRooms; i++){
            try{
                Cell originCell = mazescape
            }catch (Exception e){

            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*Room makeRooms(){
        (int width, int height) roomSize = (Random.Range(3, 8), Random.Range(3, 8));
        for(int i = 0; i < maxTries; i++){
            Room newRoom = new Room(
                mazescape[Random.Range(1, size.height - 1 - roomSize.height)][Random.Range(0, size.width - 1 - roomSize.width)],
                size
                );
            checkRoomCollision(newRoom){

            }
        }
    }

    bool checkRoomCollision(Room newRoom){
        foreach(Room room in GetComponent<Rooms>().rooms){
            if(newRoom.origin.transform
        }
    }*/
}
