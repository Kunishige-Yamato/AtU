using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titleUserInfo : MonoBehaviour
{
    Text userText;

    void Start()
    {
        if(PlayerPrefs.GetString("ID")!=null)
        {
            //textコンポーネント取得
            userText = gameObject.GetComponent<Text>();
            if (gameObject.name=="UserName")
            {
                //nameセット
                userText.text = PlayerPrefs.GetString("NAME");
            }
            else if(gameObject.name=="UserID")
            {
                //IDセット
                userText.text = PlayerPrefs.GetString("ID");
            }
        }
    }

    void Update()
    {
        
    }
}
