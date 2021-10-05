using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonConfToggle : MonoBehaviour
{
    public GameObject ConfCanvas;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Onclick()
    {
        if(gameObject.name=="ConfigurationBtn")
        {
            ConfCanvas.SetActive(true);
        }
        else
        {
            ConfCanvas.SetActive(false);
        }
    }
}
