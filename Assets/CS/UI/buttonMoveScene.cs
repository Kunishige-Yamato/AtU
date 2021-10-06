using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonMoveScene : MonoBehaviour
{
    void Start()
    {
        
    }

    void FixedUpdate()
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
                Debug.Log("achieve move");
                //SceneManager.LoadScene("achive");
                break;
            case "BackTitleButton":
                SceneManager.LoadScene("title");
                break;
        }
    }
}
