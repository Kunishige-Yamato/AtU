using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet23_3 : MonoBehaviour
{
    GameObject bulletPrefab;
    bullet23_1 bulCom;

    //落下速度
    float fallSpeed;
    //横移動速度
    float moveSpeed;
    
    void Start()
    {
        gameObject.name="bullet23-3";

        if(transform.position.x<0){
            bulletPrefab=GameObject.Find("bullet23-1(-1)");
        }
        else{
            bulletPrefab=GameObject.Find("bullet23-1(1)");
        }
        bulCom=bulletPrefab.GetComponent<bullet23_1>();


        fallSpeed=0;
        moveSpeed=0;

        Invoke("Shoot",2);
    }

    void FixedUpdate()
    {
        transform.Translate(moveSpeed,-fallSpeed,0,Space.World);

        //下まで行ったら消去
        if(transform.position.y<-7.5f){
            Destroy(gameObject);
        }
    }
    
    void Shoot()
    {
        fallSpeed=0.3f;
        moveSpeed=bulCom.moveSpeed;
    }
}
