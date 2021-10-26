using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonNext : MonoBehaviour
{
    GameObject eg;
    public GameObject totalResultCanvas;
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
            if(enemyGenerator2.gameOver)
            {
                Cursor.lockState = CursorLockMode.None;
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
            if(enemyGenerator2.gameOver==true || gameObject.name=="RetireButton")
            {
                //カーソル表示
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                //スコア画面へ
                SceneManager.LoadScene("score");
            }
            else{
                enemyGenerator2.ReadFile();
            }
        }
        else{
            if(enemyGenerator.stageNum==enemyGenerator.allStageNum)
            {
                //totlaResult表示してからスコアかタイトルへ
                if(gameObject.name == "TotalResultNextStageButton")
                {
                    //カーソル表示
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    //スコア画面へ
                    SceneManager.LoadScene("score");
                }
                else
                {
                    //カーソル表示
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    totalResultCanvas.SetActive(true);
                }
            }
            else
            {
                if(enemyGenerator.stageNum == enemyGenerator.allStageNum-1)
                {
                    nextButtonText.text = "Total Result";
                }
                enemyGenerator.ReadFile();
            }
        }
    }
}
