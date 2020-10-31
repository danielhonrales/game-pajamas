using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaster : MonoBehaviour
{

    public List<AudioSource> sounds = new List<AudioSource>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playSounds());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator playSounds(){
        sounds[Random.Range(0, sounds.Count)].Play();

        yield return new WaitForSecondsRealtime(Random.Range(5, 21));

        StartCoroutine(playSounds());
    }
}
