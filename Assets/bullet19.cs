using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet19 : MonoBehaviour
{
    //落下速度
    float fallSpeed;
    //横移動速度
    float moveSpeed;
    //速度
    float speed;

    void Start()
    {
        speed=0.03f;

        if(transform.position.y>5.5f){
            fallSpeed=speed;
        }
        else if(transform.position.x>0){
            moveSpeed=-1*speed;
        }
        else{
            moveSpeed=speed;
        }
    }

    void FixedUpdate()
    {
        transform.Translate(moveSpeed,-fallSpeed,0,Space.World);
        //端まで行ったら消去
        if(transform.position.y<-6f||transform.position.x<-10f||transform.position.x>10f){
            Destroy(gameObject);
        }
    }
}
