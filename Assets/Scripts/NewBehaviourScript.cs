using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTask : MonoBehaviour
{

    public bool generatorOn;
    public GameObject player;
    public int numberGeneratorsMax;


    // Start is called before the first frame update
    void Start()
    {
        generatorOn = false;
        numberGeneratorsMax = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool GeneratorCollison()
    {
        for (Genrator generator in generators)
        {
            if (player.position.x + player.size.width > generator.position.x &&
            player.position.x < generator.position.x + generator.size.width &&
            player.position.y + player.size.height > generator.position.y &&
            player.position.y < generator.position.y + generator.size.height)
            {
                //Debug.Log("Collision!");
                return true;
            }
        }
        //Debug.Log("Not in collision...")
        return false;
    }

    bool GeneratorTask(generatorOn)
    {
        if (GeneratorCollison() == true)
        {
            generatorOn == true;
        }
        else
        {
            generatorOn == false;
        }

        return false;
    }
}
