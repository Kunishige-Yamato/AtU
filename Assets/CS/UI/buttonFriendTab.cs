using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonFriendTab : MonoBehaviour
{
    public GameObject listTab;
    public GameObject applicationTab;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void OnClick()
    {
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
