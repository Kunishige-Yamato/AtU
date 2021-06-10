using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonNext : MonoBehaviour
{
    GameObject eg;
    EnemyGenerator enemyGenerator;

    void Start()
    {
        eg=GameObject.Find("EG");
        enemyGenerator=eg.GetComponent<EnemyGenerator>();
    }

    void FixedUpdate()
    {
        
    }

    public void OnClick()
    {
        enemyGenerator.ReadFile();
    }
}
