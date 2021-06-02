using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet20 : MonoBehaviour
{
    //落下速度
    float fallSpeed;
    //横移動速度
    float moveSpeed;
    //回転速度
    float rotSpeed;
    //タイマー
    float timer;

    void Start()
    {
        moveSpeed=Random.Range(-0.02f,0.02f);
        rotSpeed=-15f;
        timer=0;
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;
        if(timer>0.8f){
            fallSpeed+=0.002f;
            transform.Translate(moveSpeed,-fallSpeed,0,Space.World);
            transform.Rotate(0,0,rotSpeed);
        }
        //下まで行ったら消去
        if(transform.position.y<-5.5f){
            Destroy(gameObject);
        }
    }
}
