using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet24 : MonoBehaviour
{
    //落下速度
    float fallSpeed;

    void Start()
    {
        this.fallSpeed=0.1f;
    }

    void FixedUpdate()
    {
        transform.Translate(0,-fallSpeed,0,Space.World);
        //下まで行ったら消去
        if(transform.position.y<-6f){
            Destroy(gameObject);
        }
    }
}
