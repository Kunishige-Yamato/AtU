using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enemyInfo;

public class enemyInfoList
{
    //ボス型
    public Boss[] boss;
    //配列の最大値
    int arrayMax;
    //配列内の数
    int arrayNum;

    //インスタンス生成するコンストラクタ
    public enemyInfoList(int length)
    {
        //配列の初期設定
        arrayMax = length;
        arrayNum = 0;
        boss = new Boss[length];
    }

    //情報を受け取って登録するメソッド
    public void SetBossInfo(string name,float posX, float posY, int hp, int hitBonus, int defeatBonus, int timeBonus)
    {
        if(arrayNum==arrayMax)
        {
            Debug.Log("配列が満員です");
        }
        else
        {
            //boss登録
            boss[arrayNum] = new Boss(name, posX, posY, hp, hitBonus, defeatBonus, timeBonus);
            arrayNum++;
        }
    }

    //Debug用リスト表示メソッド
    public void PrintInfo()
    {
        for(int i=0;i<arrayNum;i++)
        {
            Debug.Log("ListNo." + i);
            boss[i].PrintBossInfo();
        }
    }
}
