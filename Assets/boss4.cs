using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss4 : MonoBehaviour
{
    GameObject cakePrefab;
    boss4 cakeCom;

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
    int hp=600;
    public int hit=0;
    int side;
    float angle;
    int count=0,count2=0,count3=0;

    void Start()
    {
        if(transform.position.x>0){
            gameObject.name="boss4-2Prefab";
            side=1;
            cakePrefab=GameObject.Find("boss4-1Prefab");
        }
        else{
            gameObject.name="boss4-1Prefab";
            side=-1;
            cakePrefab=GameObject.Find("boss4-2Prefab");
        }
        cakeCom=cakePrefab.GetComponent<boss4>();

        //Shoot1();
        //Shoot2();
        //Shoot3();
        //Shoot4();
        Shoot5();
    }

    void FixedUpdate()
    {
        if(cakeCom.hit>=this.hit){
            this.hit=cakeCom.hit;
        }
    }

    void Shoot1()
    {
        for(float i=-4f;i<6;i+=3){
            bulletPlace.x=9.5f*side;
            bulletPlace.y=i;
            Instantiate(bulletPrefab,bulletPlace,Quaternion.identity);
        }
    }

    void Shoot2()
    {
        angle=Random.Range(-180f,360f);
        float rad=Mathf.PI*angle/180;
        bullet2Place.x=(float)Mathf.Cos(rad)*2+transform.position.x;
        bullet2Place.y=(float)Mathf.Sin(rad)*2+transform.position.y;
        Instantiate(bullet2Prefab,bullet2Place,Quaternion.identity);
        count++;
        if(count<150){
            Invoke("Shoot2",0.08f);
        }
        else{
            count=0;
        }
    }

    void Shoot3()
    {
        if(side<0){
            bullet3Place.x=Random.Range(-5f,0);
        }
        else{
            bullet3Place.x=Random.Range(0,5f);
        }
        bullet3Place.y=7.5f;
        GameObject　go=Instantiate(bullet3Prefab,bullet3Place,Quaternion.identity) as GameObject;
        go.name="bullet23-1("+side+")";
    }

    void Shoot4()
    {
        if(transform.position.x>0){
            int i=Random.Range(-4,5)*2;
            Debug.Log(i);
            for(int j=-8;j<=8;j+=2){
                bullet4Place.x=j;
                bullet4Place.y=6f;
                if(i!=j){
                    Instantiate(bullet4Prefab,bullet4Place,Quaternion.identity);
                }
            }
            count2++;
            if(count2<6){
                Invoke("Shoot4",1.5f);
            }
            else{
                count2=0;
            }
        }

    }

    void Shoot5()
    {
        Instantiate(bullet5Prefab,transform.position,Quaternion.identity);
        count3++;
        if(count3<75){
            Invoke("Shoot5",0.15f);
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
            if(hit>this.hp)
            {
                Destroy(cakePrefab.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
