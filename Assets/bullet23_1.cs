using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet23_1 : MonoBehaviour
{
    //落下速度
    float fallSpeed;
    //横移動速度
    public float moveSpeed;

    public GameObject bulletPrefab;
    public GameObject bullet2Prefab;

    void Start()
    {
        fallSpeed=0.3f;
        moveSpeed=Random.Range(-0.3f,0.3f);
        moveSpeed-=transform.position.x/10;

        Instantiate(bullet2Prefab,transform.position,Quaternion.identity);

        Shoot();
    }

    void FixedUpdate()
    {
        transform.Translate(moveSpeed,-fallSpeed,0,Space.World);

        //下まで行ったら消去
        if(transform.position.y<-5.5f){
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab,transform.position,Quaternion.identity);
        Invoke("Shoot",0.05f);
    }
}
