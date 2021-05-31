using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2 : MonoBehaviour
{
    public GameObject bulletPrefab;
    Vector3 bulletPlace;

    void Start()
    {
        Shoot1();
    }

    void FixedUpdate()
    {
        
    }

    void Shoot1()
    {
        for(float i=-5.1f;i<5;i+=2){
            bulletPlace.x=-9.5f;
            bulletPlace.y=i;
            Instantiate(bulletPrefab,bulletPlace,Quaternion.identity);
        }
        for(float i=-9f;i<9;i+=2f){
            bulletPlace.x=i;
            bulletPlace.y=5.5f;
            Instantiate(bulletPrefab,bulletPlace,Quaternion.identity);
        }
    }
}
