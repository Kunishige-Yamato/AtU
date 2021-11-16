using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enemyInfo;

public class enemyInfoList
{
    //ボス型
    public Boss[] boss;
    //エネミー型
    public MobEnemy[] enemy;

    //配列の最大値
    int arrayMax;
    //配列内の数
    int arrayNum;

    //インスタンス生成するコンストラクタ
    public enemyInfoList(int length,string kind)
    {
        //配列の初期設定
        arrayMax = length;
        arrayNum = 0;
        if(kind=="boss")
        {
            boss = new Boss[length];
        }
        else
        {
            enemy = new MobEnemy[length];
        }
    }

    //ボスの情報を受け取って登録するメソッド
    public void SetBossInfo(string name, int hp, int hitBonus, int defeatBonus, int timeBonus)
    {
        if(arrayNum==arrayMax)
        {
            Debug.Log("配列が満員です");
        }
        else
        {
            //boss登録
            boss[arrayNum] = new Boss(name, hp, hitBonus, defeatBonus, timeBonus);
            arrayNum++;
        }
    }

    //エネミーの情報を受け取って登録するメソッド
    public void SetEnemyInfo(string name, int hp,int hitBonus,int defeatBonus, float fallSpeed, float moveSpeed, float rotSpeed, float lifeExpectancy)
    {
        if (arrayNum == arrayMax)
        {
            Debug.Log("配列が満員です");
        }
        else
        {
            //boss登録
            enemy[arrayNum] = new MobEnemy(name, hp, hitBonus, defeatBonus, fallSpeed, moveSpeed, rotSpeed, lifeExpectancy);
            arrayNum++;
        }
    }
}
