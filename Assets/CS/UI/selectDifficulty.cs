using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectDifficulty : MonoBehaviour
{
    public static int difficulty;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    public void OnClick()
    {
        switch(gameObject.name){
            case "easy":
                difficulty=0;
                break;
            case "normal":
                difficulty=1;
                break;
            case "hard":
                difficulty=2;
                break;
            case "crazy":
                difficulty=3;
                break;
        }
        SceneManager.LoadScene("Game");
    }
}
