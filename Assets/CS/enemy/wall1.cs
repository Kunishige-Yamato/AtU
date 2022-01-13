using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall1 : MonoBehaviour
{
    int hp=30;
    int hit=0;

    //SE関係
    public AudioClip audioSEClip;

    void Start()
    {
        Invoke("Des",15f);
    }

    void FixedUpdate()
    {
        
    }

    //当たったらhp減らしながら弾をブロック
    void OnTriggerEnter2D(Collider2D col)
    {
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClip);

        if (col.gameObject.tag=="Bullet")
        {
            hit++;
            if(hit>hp)
            {
                Destroy(gameObject);
            }
            Destroy(col.gameObject);
        }
    }

    void Des()
    {
        Destroy(gameObject);
    }
}