using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonSignInToggle : MonoBehaviour
{
    public GameObject signInCanvas;

    //SE関係
    public AudioClip[] audioSEClips;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    public void SignInToggle()
    {
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);

        if (signInCanvas.activeInHierarchy)
        {
            signInCanvas.SetActive(false);
        }
        else
        {
            signInCanvas.SetActive(true);
        }
    }
}
