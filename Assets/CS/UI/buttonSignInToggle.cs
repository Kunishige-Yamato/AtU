using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonSignInToggle : MonoBehaviour
{
    GameObject signInCanvas;

    void Start()
    {
        //サインイン画面取得
        signInCanvas = GameObject.Find("SignInCanvas");
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
