using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall1 : MonoBehaviour
{
    int hp=30;
    int hit=0;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    //当たったらhp減らしながら弾をブロック
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="Bullet")
        {
            hit++;
            if(hit>hp)
            {
                Destroy(gameObject);
            }
            Destroy(col.gameObject);
        }
    }
}
