using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonRestart : MonoBehaviour
{
    public CanvasGroup pauseGroup;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "loading")
        {
            SceneManager.LoadScene("game_1");
        }
    }

    void FixedUpdate()
    {
        
    }

    public void OnClick()
    {
        SceneManager.LoadScene("loading");
    }
}
