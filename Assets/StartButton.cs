using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{

    public GameObject panel;
    public GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void bye(){
        gameController.GetComponent<GameController>().gameActive = true;
        panel.SetActive(false);
    }
}
