using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternetConnectMonitor : MonoBehaviour
{
    //接続チェックするかどうか
    bool netConnectCheck = true;

    public GameObject popUpPrefab;
    GameObject popUp;

    //SE関係
    public AudioClip[] audioSEClips;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(netConnectCheck)
        {
            // ネットワークの状態を確認する
            if (Application.internetReachability == NetworkReachability.NotReachable && popUp == null) 
            {
                // ネットワークに接続されていない状態
                //SE再生
                GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);

                //ポップアップ作成，テキストとボタンの設定
                popUp = Instantiate(popUpPrefab, new Vector3(0, 0, 0), Quaternion.identity);

                //テキスト設定
                popUp.transform.Find("TextBackGround/Text").GetComponent<Text>().text = "You don't seem to be connected to the internet. Check your internet connection.";

                //ボタン1設定
                GameObject button_1 = popUp.transform.Find("Buttons/Button_1").gameObject;
                //ボタンの背景色設定
                button_1.GetComponent<Image>().color = Color.gray;
                //ボタンのテキスト変更
                button_1.transform.Find("Text").GetComponent<Text>().text = "OK";

                //ボタン2設定
                GameObject button_2 = popUp.transform.Find("Buttons/Button_2").gameObject;
                Destroy(button_2);
            }
        }
    }

    public void InternetConectCheckOn()
    {
        netConnectCheck = true;
    }

    public void InternetConnectCheckOff()
    {
        netConnectCheck = false;
    }
}
