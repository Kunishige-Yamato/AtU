using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enemyInfo
{
    //基底クラス
    [System.Serializable]
    public class Enemy
    {
        //出現座標
        public float posX;
        public float posY;
        //体力
        public int hp;
        //被弾，撃沈スコア
        public int hitBonus;
        public int defeatBonus;

        public Enemy(float posX, float posY, int hp,int hitBonus,int defeatBonus)
        {
            this.posX = posX;
            this.posY = posY;
            this.hp = hp;
            this.hitBonus = hitBonus;
            this.defeatBonus = defeatBonus;
        }   
    }

    //派生クラス
    [System.Serializable]
    public class Boss : Enemy
    {
        public string name;
        public int timeBonus;
        public GameObject bossObj;

        public Boss(string name, float posX, float posY, int hp, int hitBonus, int defeatBonus,int timeBonus) : base(posX, posY, hp, hitBonus, defeatBonus)
        {
            this.name = name;
            this.timeBonus = timeBonus;
            bossObj = Resources.Load("Prefabs/Boss/"+this.name) as GameObject;
        }

        //debug用中身表示メソッド
        public void PrintBossInfo()
        {
            Debug.Log(name + "," + bossObj.name + "," + posX + "," + posY + "," + hp + "," + hitBonus + "," + defeatBonus + "," + timeBonus);
        }
    }
}