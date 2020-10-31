using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    public bool generatorOn;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        generatorOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onTriggerEnter(Collider other)
    {
        if(other.transform.parent == player){
            generatorOn = true;
            
        }
    }
}
