using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy3 : MonoBehaviour
{
    public GameObject bulletPrefab;
    Vector3 bulletPlace,objPlace;
    public int num;
    int hit=0;
    float timer;
    int hp=6;

    void Start()
    {
        bulletPlace.x=gameObject.transform.position.x+0.1f;
        bulletPlace.y=gameObject.transform.position.y-1;
        bulletPlace.z=gameObject.transform.position.z;
        num=0;
        StartCoroutine(Shoot(-0.15f));
        StartCoroutine(Shoot(-0.05f));
        StartCoroutine(Shoot(0.05f));
        StartCoroutine(Shoot(0.15f));

        timer=0;
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;

        if(timer>=10){
            Destroy(gameObject);
        }
    }

    private IEnumerator Shoot(float mov)
    {
        int i=0;
        //無限に打ち続ける
        while(i<1){
            GameObject mb=Instantiate(bulletPrefab,bulletPlace,Quaternion.identity);
            mb.name="bullet_"+num+transform.position.x*100;
            GameObject bul=GameObject.Find("bullet_"+num+transform.position.x*100);
            num++;
            bullet2 b2=bul.GetComponent<bullet2>();
            b2.moveSpeed=mov;

            yield return new WaitForSeconds(1.2f);
        }
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
