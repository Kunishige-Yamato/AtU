using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigAdmin : MonoBehaviour
{
    public GameObject creditObj;

    //名前変更
    public InputField inputField;
    public Text errorText;

    void Start()
    {
        //クレジット記述
        ReadCredit();

        //名前変更準備
        inputField = inputField.GetComponent<InputField>();
    }

    void Update()
    {
        
    }

    void OnEnable()
    {
        errorText.text = "If user name is changed successfully, it will be reloaded.";
    }

    void ReadCredit()
    {
        //コンポーネント取得
        Text cresitText = creditObj.GetComponent<Text>();

        //クレジットの中身をファイルから取得
        TextAsset textFile = Resources.Load<TextAsset>("Text/credit");

        cresitText.text = textFile.text;
    }

    public void renameUserName()
    {
        //エンターキーで確定
        if (inputField.text.IndexOf("\n") == -1)
        {
            return;
        }

        //falseだったら不可
        string name = inputField.text.Trim();
        if (name == "false") 
        {
            errorText.text = "Characters that cannot be used in user name are set.";
        }
        else
        {
            jsonReceive jsonRec = new jsonReceive();
            StartCoroutine(jsonRec.RenameUserName(PlayerPrefs.GetString("ID"), name, errorText));
        }

        //リセット
        inputField.text = "";
    }
}
