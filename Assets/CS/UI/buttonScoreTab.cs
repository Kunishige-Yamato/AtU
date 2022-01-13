using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScoreTab : MonoBehaviour
{
    public GameObject myScoreTab;
    public GameObject FriendTab;
    public GameObject AllWorldTab;

    //SE関係
    public AudioClip[] audioSEClips;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    public void OnClick()
    {
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);

        switch (gameObject.name)
        {
            case "MyScoreBtn":
                myScoreTab.SetActive(true);
                FriendTab.SetActive(false);
                AllWorldTab.SetActive(false);
                break;
            case "FriendScoreBtn":
                myScoreTab.SetActive(false);
                FriendTab.SetActive(true);
                AllWorldTab.SetActive(false);
                break;
            case "AllScoreBtn":
                myScoreTab.SetActive(false);
                FriendTab.SetActive(false);
                AllWorldTab.SetActive(true);
                break;
        }
    }
}
