using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreTabButton : MonoBehaviour
{
    public GameObject myScoreTab;
    public GameObject FriendTab;
    public GameObject AllWorldTab;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    public void OnClick()
    {
        switch(gameObject.name)
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
