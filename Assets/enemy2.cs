using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2 : MonoBehaviour
{
    public GameObject bulletPrefab;
    Vector3 bulletPlace,objPlace;
    bool direction;
    int hit=0;
    int hp=2;

    void Start()
    {
        bulletPlace.x=gameObject.transform.position.x;
        bulletPlace.y=gameObject.transform.position.y-1;
        Invoke("Des",5.0f);
        Shoot();
    }

    void FixedUpdate()
    {
        objPlace=transform.position;
        float moveSpeed=Random.Range(0.1f,0.3f);
        if(objPlace.x<7&&direction==true){
            objPlace.x+=moveSpeed;
        }
        else{
            direction=false;
        }
        if(objPlace.x>-7&&direction==false){
            objPlace.x-=moveSpeed;
        }
        else{
            direction=true;
        }
        transform.position=objPlace;
        bulletPlace.x=gameObject.transform.position.x;
        bulletPlace.y=gameObject.transform.position.y-1;
    }

    void Shoot()
    {
        Invoke("Shoot",0.4f);
        Instantiate(bulletPrefab,bulletPlace,Quaternion.identity);
    }

    void Des()
    {
        Destroy(gameObject);
    }

    //当たったら消去
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="Bullet")
        {
            hit++;
            Destroy(col.gameObject);
            if(hit>hp){
                Destroy(gameObject);
            }
        }
    }
}
