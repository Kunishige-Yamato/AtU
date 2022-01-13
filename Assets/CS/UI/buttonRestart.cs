using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonRestart : MonoBehaviour
{
    public CanvasGroup pauseGroup;

    //SE関係
    public AudioClip[] audioSEClips;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "loading")
        {
            SceneManager.LoadScene("game_1");
        }
    }

    void FixedUpdate()
    {
        
    }

    public void OnClick()
    {
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);

        SceneManager.LoadScene("loading");
    }
}
