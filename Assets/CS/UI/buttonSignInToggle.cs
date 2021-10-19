using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonSignInToggle : MonoBehaviour
{
    public GameObject signInCanvas;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    public void SignInToggle()
    {
        if(signInCanvas.activeInHierarchy)
        {
            signInCanvas.SetActive(false);
        }
        else
        {
            signInCanvas.SetActive(true);
        }
    }
}
