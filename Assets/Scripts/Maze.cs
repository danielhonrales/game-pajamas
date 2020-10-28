using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{

    (int width, int height) size = (30, 30);
    (int x, int y) origin = (0, 0);
    GameObject[,] mazescape;
    
    public GameObject cellPrefab;

    public int maxTries;
    public int numRooms;
    public List<Room> rooms;

    public Sprite openSprite;
    public Sprite closedSprite;

    // Start is called before the first frame update
    void Start()
    {
        cellPrefab.GetComponent<Cell>().size = 32;

        maxTries = 30;
        numRooms = 0;
        rooms = new List<Room>();
        
        // Generate new closed maze
        generateNewMaze();

        // Generate rooms
        makeRooms();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generateNewMaze(){
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
    }

    void makeRooms(){
        // Generate random room (until maximum tries)
        for(int k = 0; k < maxTries; k++){
            (int width, int height) roomSize = (Random.Range(3, 8), Random.Range(3, 8));
            (int x, int y) position = (Random.Range(1, size.width - roomSize.width), Random.Range(1, size.height - roomSize.height));
            Room newRoom = new Room(mazescape[position.y, position.x], position, roomSize);

            // Check for collisions
            if(!roomCollision(newRoom)){
                rooms.Add(newRoom);

                // Hollow out new room
                for(int i = newRoom.position.y; i < newRoom.size.height + newRoom.position.y - 1; i++){
                    for(int j = newRoom.position.x; j < newRoom.size.width + newRoom.position.x - 1; j++){
                        mazescape[i, j].GetComponent<Cell>().closed = false;
                        mazescape[i, j].GetComponent<SpriteRenderer>().sprite = openSprite;
                    }
                }
            }
        }
    }

    bool roomCollision(Room newRoom){
        foreach(Room room in rooms){
            if(newRoom.position.x + newRoom.size.width > room.position.x &&
            newRoom.position.x < room.position.x + room.size.width &&
            newRoom.position.y + newRoom.size.height > room.position.y &&
            newRoom.position.y < room.position.y + room.size.height){
                Debug.Log("Collision!");
                return true;
            } 
        }
        Debug.Log("New room found...");
        return false;
    }



    public class Room {

        public GameObject originCell;
        public (int x, int y) position;
        public (int width, int height) size;

        public Room(GameObject originCell, (int x, int y) position, (int width, int height) size){
            this.originCell = originCell;
            this.position = position;
            this.size = size;
        }
    }
}
