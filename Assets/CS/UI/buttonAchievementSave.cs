using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonAchievementSave : MonoBehaviour
{
    jsonReceive jsonRec = new jsonReceive();
    int s_num,t_num;

    void Start()
    {

    }

    void Update()
    {
        
    }

    //変更した場合番号取得
    public void GetSkinNum(int skinNum)
    {
        s_num = skinNum;
    }

    public void GetTitleNum(int titleNum)
    {
        t_num = titleNum;
    }

    public void SaveDecoration()
    {
        if(s_num!=0&&t_num!=0)
        {
            Debug.Log(s_num + "," + t_num);
            //StartCoroutine(jsonRec.SaveDecoration(PlayerPrefs.GetString("ID"),s_num,t_num));
        }
        else
        {
            Debug.Log("not change");
        }
    }
}
