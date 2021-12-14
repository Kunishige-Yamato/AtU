﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonSignOut : MonoBehaviour
{
    public GameObject popUpPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClick()
    {
        //ポップアップ作成，テキストとボタンの設定
        GameObject popUp = Instantiate(popUpPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        //テキスト設定
        popUp.transform.Find("TextBackGround/Text").GetComponent<Text>().text = "Do you want to sign out?";

        //ボタン1設定
        GameObject button_1 = popUp.transform.Find("Buttons/Button_1").gameObject;
        //ボタンの背景色設定
        button_1.GetComponent<Image>().color = Color.gray;
        //ボタンのテキスト変更
        button_1.transform.Find("Text").GetComponent<Text>().text = "Cancel";

        //ボタン2設定
        GameObject button_2 = popUp.transform.Find("Buttons/Button_2").gameObject;
        //ボタン押した時動くメソッドを追加
        button_2.GetComponent<Button>().onClick.AddListener(SignOut);
        //ボタンの背景色設定
        button_2.GetComponent<Image>().color = Color.red;
        //ボタンのテキスト変更
        button_2.transform.Find("Text").GetComponent<Text>().text = "Sign Out";

        //ボタンが1つでいい場合は「button_2」を取得後，以下を実行
        //Destroy(button_2);
    }

    public void SignOut()
    {
        //端末保存データすべて削除
        PlayerPrefs.DeleteAll();
        //指定したキーだけを削除
        //PlayerPrefs.DeleteKey("key");

        //サインアップシーン移動
        SceneManager.LoadScene("signUp");
    }
}
