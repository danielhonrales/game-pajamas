using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class JumpScare : MonoBehaviour {
 
    public Image image;
    public AudioSource audioSource;
 
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            Application.Quit();
        }
    }
 
    IEnumerator JumpScare_CR()
    {
        image.enabled = true;
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);
        image.enabled = false;
    }
}