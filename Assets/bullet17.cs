using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet17 : MonoBehaviour
{
    //落下速度
    float fallSpeed;
    //横移動速度
    float moveSpeed;
    //回転速度
    float rotSpeed;

    void Start()
    {
        fallSpeed=transform.position.y/5;
        moveSpeed=transform.position.x/5;
    }

    void FixedUpdate()
    {
        transform.Translate(moveSpeed,fallSpeed,0,Space.World);

        //下まで行ったら消去
        if(transform.position.y<-5.5f||transform.position.y>5.5f||transform.position.x<-9.5f||transform.position.x>9.5f){
            Destroy(gameObject);
        }
    }
}
