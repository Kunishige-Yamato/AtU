using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonSignOut : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClick()
    {
        //端末保存データすべて削除
        PlayerPrefs.DeleteAll();
        //指定したキーだけを削除
        //PlayerPrefs.DeleteKey("key");

        //サインアップシーン移動
        SceneManager.LoadScene("signUp");
    }
}
