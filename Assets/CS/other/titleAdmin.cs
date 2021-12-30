using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class titleAdmin : MonoBehaviour
{
    public GameObject audioPrefab;
    public AudioClip defaultClip;

    void Start()
    {
        GameObject mainAudio = GameObject.Find("AudioObj");
        if(mainAudio==null)
        {
            mainAudio = Instantiate(audioPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            mainAudio.name = "AudioObj";
            DontDestroyOnLoad(mainAudio);
        }
        mainAudio.GetComponent<AudioSource>().clip = defaultClip;
    }

    void Update()
    {
        
    }
}
