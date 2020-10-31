using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelEscape : MonoBehaviour
{

    public GameObject escapePrompt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onClick(){
        escapePrompt.SetActive(false);
    }
}
