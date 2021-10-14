using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonCanvasToggle : MonoBehaviour
{
    public GameObject ConfCanvas;
    public GameObject FriCanvas;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Onclick()
    {
        if (gameObject.name == "ConfigurationBtn")
        {
            ConfCanvas.SetActive(true);
        }
        else if (gameObject.name == "FriendBtn")
        {
            FriCanvas.SetActive(true);
        }
        else
        {
            ConfCanvas.SetActive(false);
            FriCanvas.SetActive(false);
        }
    }
}
