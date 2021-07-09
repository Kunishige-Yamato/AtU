using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet16_2 : MonoBehaviour
{
    //bullet14-1
    GameObject bulletPrefab;
    bullet16_1 bul;

    //自分の大きさ
    Vector3 bulletScale;

    void Start()
    {
        bulletPrefab=GameObject.Find("bullet16-1Prefab(Clone)");
        bul=bulletPrefab.GetComponent<bullet16_1>();
    }

    void FixedUpdate()
    {
        //縮小
        bulletScale=transform.localScale;
        bulletScale.x-=0.0005f;
        bulletScale.y-=0.0005f;
        transform.localScale=bulletScale;

        //消滅
        if(transform.localScale.x<0.02||transform.localScale.y<0.02){
            Destroy(gameObject);
        }
        
        if(bul.del==true){
            Destroy(gameObject);
        }
    }
}
