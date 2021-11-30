using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonRestart : MonoBehaviour
{
    public CanvasGroup pauseGroup;

    void Start()
    {

    }

    void FixedUpdate()
    {
        
    }

    public void OnClick()
    {
        Clear();
    }

    void Clear()
    {
        //タグつきを全て格納
        GameObject[] enemys=GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject del in enemys) {
            Destroy(del);
        }
        GameObject[] bullets=GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject del in enemys) {
            Destroy(del);
        }
    }
}
