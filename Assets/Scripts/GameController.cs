using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public bool gameActive = false;
    public GameObject escapePrompt;

    public int generators = 0;
    public int generatorsOn = -1;

    public GameObject gate;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(800, 800, true);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            escapePrompt.SetActive(!escapePrompt.activeSelf);
        }

        if(generatorsOn == generators){
            gate.SetActive(false);
        }
    }
}
