using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonFriendTab : MonoBehaviour
{
    public GameObject listTab;
    public GameObject applicationTab;

    //SE関係
    public AudioClip[] audioSEClips;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void OnClick()
    {
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);

        switch (gameObject.name)
        {
            case "FriendListBtn":
                listTab.SetActive(true);
                applicationTab.SetActive(false);
                break;
            case "ApplicationBtn":
                listTab.SetActive(false);
                applicationTab.SetActive(true);
                break;
        }
    }
}
