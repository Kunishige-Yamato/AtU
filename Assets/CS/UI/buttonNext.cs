using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if(selectDifficulty.endless){
            if(enemyGenerator2.gameOver){
                nextButtonText.text="Total Result";
            }

            if (enemyGenerator2.gameOver && gameObject.name == "RetireButton")
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnClick()
    {
        Cursor.lockState=CursorLockMode.Locked;
        if(selectDifficulty.endless){
            if(enemyGenerator2.gameOver==true || gameObject.name=="RetireButton"){
                //スコア画面へ
                SceneManager.LoadScene("score");
            }
            else{
                enemyGenerator2.ReadFile();
            }
        }
        else{
            if(enemyGenerator.stageNum==enemyGenerator.allStageNum){
                //スコア画面へ
                SceneManager.LoadScene("score");
            }
            else
            {
                nextButtonText.text = "Total Result";
                enemyGenerator.ReadFile();
            }
        }
    }
}
