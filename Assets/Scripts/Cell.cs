using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    public float size;
    public bool closed;

    // Start is called before the first frame update
    void Start()
    {
        size = this.gameObject.GetComponent<SpriteRenderer>().sprite.rect.width;
        closed = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
