using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enemyInfo;

public class enemyAppearance : MonoBehaviour
{
    Boss bossInfo;
    bossBasicInfo basicInfo;
    GameObject enemyObj;

    public void Appearance(Boss bossInfo)
    {
        //bossオブジェクトを画面に生成する
        enemyObj = Instantiate(bossInfo.bossObj, new Vector3(bossInfo.posX, bossInfo.posY, 0), Quaternion.identity);
        enemyObj.name = bossInfo.name;
        basicInfo = enemyObj.GetComponent<bossBasicInfo>();
        basicInfo.SetBasicInfo(bossInfo.name,bossInfo.hp,bossInfo.hitBonus,bossInfo.defeatBonus,bossInfo.timeBonus);
        //enemyObj.GetComponent<boss1>().hp = hp;
    }
}
