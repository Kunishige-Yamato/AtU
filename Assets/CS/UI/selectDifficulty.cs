using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectDifficulty : MonoBehaviour
{
    int difficulty;
    int endlessMode;

    public void OnClick()
    {
        switch(gameObject.name){
            case "easy":
                PlayerPrefs.SetInt("endless", 0);
                PlayerPrefs.SetInt("difficulty", 0);
                break;
            case "normal":
                PlayerPrefs.SetInt("endless", 0);
                PlayerPrefs.SetInt("difficulty", 1);
                break;
            case "hard":
                PlayerPrefs.SetInt("endless", 0);
                PlayerPrefs.SetInt("difficulty", 2);
                break;
            case "crazy":
                PlayerPrefs.SetInt("endless", 0);
                PlayerPrefs.SetInt("difficulty", 3);
                break;
            case "endlessNormal":
                PlayerPrefs.SetInt("difficulty", 0);
                PlayerPrefs.SetInt("endless", 1);
                PlayerPrefs.SetInt("gambling", 0);
                break;
            case "endlessGambling":
                PlayerPrefs.SetInt("difficulty", 0);
                PlayerPrefs.SetInt("endless", 1);
                PlayerPrefs.SetInt("gambling", 1);
                break;
        }
        PlayerPrefs.Save();
        Cursor.lockState=CursorLockMode.Locked;
        SceneManager.LoadScene("game_1");
    }
}
