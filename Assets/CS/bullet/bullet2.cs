using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet2 : MonoBehaviour
{
    //落下速度
    float fallSpeed;
    //横移動速度
    public float moveSpeed=0;
    //回転速度
    float rotSpeed;

    void Start()
    {
        fallSpeed=0.1f;
        rotSpeed=5f;
    }

    void FixedUpdate()
    {
        transform.Translate(moveSpeed,-fallSpeed,0,Space.World);
        transform.Rotate(0,0,rotSpeed);
        //下まで行ったら消去
        if(transform.position.y<-5.5f){
            Destroy(gameObject);
        }
    }
}