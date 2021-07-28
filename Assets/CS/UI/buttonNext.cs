using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonNext : MonoBehaviour
{
    GameObject eg;
    EnemyGenerator enemyGenerator;
    EnemyGenerator2 enemyGenerator2;
    public Text nextButtonText;

    void Start()
    {
        eg=GameObject.Find("EG");
        if(selectDifficulty.endless){
            enemyGenerator2=eg.GetComponent<EnemyGenerator2>();
        }
        else{
            enemyGenerator=eg.GetComponent<EnemyGenerator>();
        }
    }

    void FixedUpdate()
    {
        
    }

    public void OnClick()
    {
        Cursor.lockState=CursorLockMode.Locked;
        if(selectDifficulty.endless){
            enemyGenerator2.ReadFile();
        }
        else{
            if(enemyGenerator.stageNum==enemyGenerator.allStageNum-1){
                nextButtonText.text="Total Result";
                enemyGenerator.ReadFile();
            }
            else{
                enemyGenerator.ReadFile();
            }
        }
    }
}
