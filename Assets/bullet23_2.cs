using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet23_2 : MonoBehaviour
{
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    //当たったら消去
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("hit");
        if(col.gameObject.name=="bullet23-3")
        {
            Debug.Log("delete");
            Destroy(gameObject);
        }
    }
}
