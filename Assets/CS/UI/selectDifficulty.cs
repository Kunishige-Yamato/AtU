using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectDifficulty : MonoBehaviour
{
    public static int difficulty;
    public static int endlessMode;
    public static bool endless;

    void Start()
    {
        endless=false;
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
            case "endlessNormal":
                difficulty=0;
                endlessMode=0;
                endless=true;
                break;
            case "endlessGambling":
                difficulty=0;
                endlessMode=1;
                endless=true;
                break;
        }
        Cursor.lockState=CursorLockMode.Locked;
        if(endless){
            SceneManager.LoadScene("Endless");
        }
        else{
            SceneManager.LoadScene("Game");
        }
    }
}
