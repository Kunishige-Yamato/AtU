using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingAdmin : MonoBehaviour
{
    public GameObject scorePrefab;

    void Start()
    {
        AllWorldRanking();
    }

    void FixedUpdate()
    {
        
    }

    void AllWorldRanking()
    {
        for(int i=0;i<10;i++)
        {
            Instantiate(scorePrefab,new Vector3(0,0,0),Quaternion.identity);
        }
    }
}
