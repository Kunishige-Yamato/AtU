using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleAdmin : MonoBehaviour
{
    public GameObject audioPrefab;

    void Start()
    {
        GameObject mainAudio = GameObject.Find("AudioObj");
        if(mainAudio==null)
        {
            mainAudio = Instantiate(audioPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            mainAudio.name = "AudioObj";
            DontDestroyOnLoad(mainAudio);
        }
    }

    void Update()
    {
        
    }
}
