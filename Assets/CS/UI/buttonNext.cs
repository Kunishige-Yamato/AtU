using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonNext : MonoBehaviour
{
    GameObject eg;
    EnemyGenerator enemyGenerator;
    public Text nextButtonText;

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
        Cursor.lockState=CursorLockMode.Locked;
        if(enemyGenerator.stageNum==enemyGenerator.allStageNum-1){
            nextButtonText.text="Total Result";
            enemyGenerator.ReadFile();
        }
        else{
            enemyGenerator.ReadFile();
        }
    }
}
