using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bullet2Prefab;
    public GameObject bullet3Prefab;
    public GameObject bullet4Prefab;
    public GameObject bullet5Prefab;
    Vector3 bulletPlace;
    Vector3 bullet2Place;
    Vector3 bullet3Place;
    Vector3 bullet4Place;
    Vector3 bullet5Place;
    int count=0;
    int count2=0;
    int count3=0;
    bool direction;
    Vector3 objPlace;
    int hp=600;
    int hit=0;

    void Start()
    {
        Invoke("Shoot1",0f);
        Invoke("Shoot2",10f);
        Invoke("Shoot3",25f);
        Invoke("Shoot4",40f);
        Invoke("Shoot5",49f);
    }

    void FixedUpdate()
    {
        float moveSpeed=0.01f;
        objPlace=transform.position;
        if(objPlace.x<4&&direction==true){
            objPlace.x+=moveSpeed;
        }
        else{
            direction=false;
        }
        if(objPlace.x>-4&&direction==false){
            objPlace.x-=moveSpeed;
        }
        else{
            direction=true;
        }
        transform.position=objPlace;
    }

    void Shoot1()
    {
        bulletPlace.x=Random.Range(-9,9);
        bulletPlace.y=5.5f;
        Instantiate(bulletPrefab,bulletPlace,Quaternion.identity);
        if(count<80){
            Invoke("Shoot1",0.1f);
            count++;
        }
        else{
            count=0;
        }
    }

    void Shoot2()
    {
        for(int i=-8;i<=8;i+=2){
            bullet2Place.x=i;
            bullet2Place.y=5.5f;
            Instantiate(bullet2Prefab,bullet2Place,Quaternion.identity);
        }
    }

    void Shoot3()
    {
        for(float i=-6;i<=2;i+=1.5f){
            bullet3Place.x=-9.5f;
            bullet3Place.y=i;
            Instantiate(bullet3Prefab,bullet3Place,Quaternion.identity);
        }
        if(count2<2){
            Invoke("Shoot3",3.8f);
            count2++;
        }
        else{
            count2=0;
        }

    }

    void Shoot4()
    {
        for(float i=-5.1f;i<5;i+=2){
            bullet4Place.x=-9.5f;
            bullet4Place.y=i;
            Instantiate(bullet4Prefab,bullet4Place,Quaternion.identity);
        }
        for(float i=-9f;i<9;i+=2f){
            bullet4Place.x=i;
            bullet4Place.y=5.5f;
            Instantiate(bullet4Prefab,bullet4Place,Quaternion.identity);
        }
    }

    void Shoot5()
    {
        bullet5Place.x=Random.Range(-8f,8f);
        bullet5Place.y=-5.5f;
        Instantiate(bullet5Prefab,bullet5Place,Quaternion.identity);
        if(count3<80){
            Invoke("Shoot5",0.2f);
            count3++;
        }
        else{
            count3=0;
        }
    }

    //当たったら消去
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="Bullet")
        {
            hit++;
            if(hit>hp)
            {
                Destroy(gameObject);
            }
        }
    }
}
