using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Maze : MonoBehaviour
{

    (int width, int height) size = (5, 5);
    (int x, int y) origin = (0, 0);
    GameObject[,] mazescape;
    
    public GameObject cellPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Generate new closed maze
        mazescape = new GameObject[size.height, size.width];

        for(int i = 0; i < mazescape.GetLength(0); i++){
            for(int j = 0; j < mazescape.GetLength(1); j++){
                Debug.Log(new Vector3(origin.x + (j * cellPrefab.GetComponent<Cell>().size), origin.y + (i * cellPrefab.GetComponent<Cell>().size), 0));
                mazescape[i,j] = Instantiate(cellPrefab, new Vector3(origin.x + (j * cellPrefab.GetComponent<Cell>().size), origin.y + (i * cellPrefab.GetComponent<Cell>().size), 0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
