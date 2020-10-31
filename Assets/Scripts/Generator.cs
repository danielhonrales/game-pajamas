using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    public bool generatorOn;
    public bool visited;
    public GameObject player;
    public GameObject gameController;
    public Sprite generatorOnSprite;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController");
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && !visited){
            gameController.GetComponent<GameController>().generatorsOn++;
            GetComponent<SpriteRenderer>().sprite = generatorOnSprite;
            visited = true;
        }
    }
}
