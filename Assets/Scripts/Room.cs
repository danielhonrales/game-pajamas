using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    Cell origin;
    (int width, int height) size;

    Room(Cell origin, (int width, int height) size){
        this.origin = origin;
        this.size = size;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
