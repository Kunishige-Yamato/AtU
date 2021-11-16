using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enemyInfo;

public class enemyAppearance : MonoBehaviour
{
    //使う部品
    MobEnemy enemyInfo;
    bossBasicInfo bossBasicInfo;
    enemyBasicInfo enemyBasicInfo;
    GameObject enemyObj;

    public void BossAppearance(Boss bossInfo, float posX, float posY)
    {
        //bossオブジェクトを画面に生成する
        enemyObj = Instantiate(bossInfo.bossObj, new Vector3(posX, posY, 0), Quaternion.identity);
        enemyObj.name = bossInfo.name;
        bossBasicInfo = enemyObj.GetComponent<bossBasicInfo>();
        bossBasicInfo.SetBasicInfo(bossInfo.name,bossInfo.hp,bossInfo.hitBonus,bossInfo.defeatBonus,bossInfo.timeBonus);
        //enemyObj.GetComponent<boss1>().hp = hp;
    }

    public void EnemyAppearance(MobEnemy enemyInfo, float posX, float posY)
    {
        //enemyオブジェクトを画面に生成する
        enemyObj = Instantiate(enemyInfo.enemyObj, new Vector3(posX, posY, 0), Quaternion.identity);
        enemyObj.name = enemyInfo.name;
        enemyBasicInfo = enemyObj.GetComponent<enemyBasicInfo>();
        enemyBasicInfo.SetBasicInfo(enemyInfo.name, enemyInfo.hp, enemyInfo.hitBonus, enemyInfo.defeatBonus, enemyInfo.fallSpeed, enemyInfo.moveSpeed, enemyInfo.rotSpeed, enemyInfo.lifeExpectancy);
        //enemyObj.GetComponent<boss1>().hp = hp;
    }
}
