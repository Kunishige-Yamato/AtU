using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet13 : MonoBehaviour
{
    //落下速度
    float fallSpeed;
    //横移動速度
    float moveSpeed;
    //回転速度
    float rotSpeed;

    void Start()
    {
        moveSpeed=0.08f;
        rotSpeed=3f;
        Down();
    }

    void FixedUpdate()
    {
        transform.Translate(moveSpeed,-fallSpeed,0,Space.World);
        transform.Rotate(0,0,rotSpeed);
        //右まで行ったら消去
        if(transform.position.x>9.5f){
            Destroy(gameObject);
        }
    }

    void Up()
    {
        fallSpeed=0.1f;
        Invoke("Down",0.8f);
    }

    void Down()
    {
        fallSpeed=-0.1f;
        Invoke("Up",0.8f);
    }
}
