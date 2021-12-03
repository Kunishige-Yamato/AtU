using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class buttonMoveScene : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void Onclick()
    {
        switch(gameObject.name){
            case "StartBtn":
                SceneManager.LoadScene("selectMode");
                break;
            case "ScoreBtn":
                SceneManager.LoadScene("score");
                break;
            case "AchievementBtn":
                SceneManager.LoadScene("achievement");
                break;
            default:
                if(SceneManager.GetActiveScene().name=="game_1")
                {
                    GameObject.Find("Progress").GetComponent<progress>().SaveScore();
                }
                SceneManager.LoadScene("title");
                break;
        }
    }
}
