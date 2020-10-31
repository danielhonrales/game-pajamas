using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    public float size;
    public bool closed;
    public bool visited;
    public Sprite openSprite;
    public Sprite closedSprite;

    // Start is called before the first frame update
    void Start()
    {
        size = this.gameObject.GetComponent<SpriteRenderer>().sprite.rect.width;
        closed = true;
        visited = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void open(){
        closed = false;
        GetComponent<SpriteRenderer>().sprite = openSprite;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
