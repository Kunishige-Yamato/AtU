using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet1 : MonoBehaviour
{
    //落下速度
    float fallSpeed;
    //回転速度
    float rotSpeed;

    void Start()
    {
        this.fallSpeed=0.05f;
        this.rotSpeed=15f;
    }

    void FixedUpdate()
    {
        transform.Translate(0,-fallSpeed,0,Space.World);
        transform.Rotate(0,0,rotSpeed);
        //下まで行ったら消去
        if(transform.position.y<-5.5f){
            Destroy(gameObject);
        }
    }
}
