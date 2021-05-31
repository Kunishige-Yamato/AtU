using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet14_2 : MonoBehaviour
{
    float timer;

    void Start()
    {
        timer=0;
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;

        if(timer>3.5f){
            Destroy(gameObject);
        }
    }
}
