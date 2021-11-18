using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet6 : MonoBehaviour
{
    //落下速度
    float fallSpeed;
    //横移動速度
    public float moveSpeed=0;
    //回転速度
    float rotSpeed;

    void Start()
    {
        fallSpeed=0.03f;
        moveSpeed = Random.Range(-0.03f, 0.03f);
        rotSpeed=0;
    }

    void FixedUpdate()
    {
        GameObject pl=GameObject.Find("Player");
        if(gameObject.transform.position.y>1)
        {
            moveSpeed += 0;
        }
        else if(gameObject.transform.position.y>4){
            if(pl.transform.position.x>gameObject.transform.position.x)
            {
                moveSpeed+=0.003f;
            }
            else if(pl.transform.position.x<gameObject.transform.position.x)
            {
                moveSpeed-=0.003f;
            }
            else{
                moveSpeed=0;
            }
        }
        else if(gameObject.transform.position.y>-1)
        {
            if(pl.transform.position.x>gameObject.transform.position.x)
            {
                moveSpeed+=0.006f;
            }
            else if(pl.transform.position.x<gameObject.transform.position.x)
            {
                moveSpeed-=0.006f;
            }
            else{
                moveSpeed+=0;
            }
        }
        
        transform.Translate(moveSpeed,-fallSpeed,0,Space.World);
        transform.Rotate(0,0,rotSpeed);
        //下まで行ったら消去
        if(transform.position.y<-5.5f){
            Destroy(gameObject);
        }
    }
}
