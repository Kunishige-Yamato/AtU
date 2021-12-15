using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigAdmin : MonoBehaviour
{
    public GameObject creditObj;

    void Start()
    {
        //クレジット記述
        ReadCredit();
    }

    void Update()
    {
        
    }

    void ReadCredit()
    {
        //コンポーネント取得
        Text cresitText = creditObj.GetComponent<Text>();

        //クレジットの中身をファイルから取得
        TextAsset textFile = Resources.Load<TextAsset>("Text/credit");

        cresitText.text = textFile.text;
    }
}
