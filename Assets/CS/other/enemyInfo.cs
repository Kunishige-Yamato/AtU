using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enemyInfo
{
    //基底クラス
    [System.Serializable]
    public class Enemy
    {
        //名前
        public string name;
        //体力
        public int hp;
        //被弾，撃沈スコア
        public int hitBonus;
        public int defeatBonus;

        public Enemy(string name, int hp,int hitBonus,int defeatBonus)
        {
            this.name = name;
            this.hp = hp;
            this.hitBonus = hitBonus;
            this.defeatBonus = defeatBonus;
        }   
    }

    //派生ボスクラス
    [System.Serializable]
    public class Boss : Enemy
    {
        public int timeBonus;
        public GameObject bossObj;

        public Boss(string name, int hp, int hitBonus, int defeatBonus,int timeBonus) : base(name, hp, hitBonus, defeatBonus)
        {
            this.timeBonus = timeBonus;
            bossObj = Resources.Load("Prefabs/Boss/" + this.name) as GameObject;
        }
    }

    //派生モブエネミークラス
    [System.Serializable]
    public class MobEnemy : Enemy
    {
        //上下左右の移動速度
        public float fallSpeed;
        public float moveSpeed;
        //回転の速度
        public float rotSpeed;

        //オブジェクト
        public GameObject enemyObj;

        //余命
        public float lifeExpectancy;

        public MobEnemy(string name, int hp, int hitBonus, int defeatBonus, float fallSpeed, float moveSpeed, float rotSpeed, float lifeExpectancy) : base(name, hp, hitBonus, defeatBonus)
        {
            this.fallSpeed = fallSpeed;
            this.moveSpeed = moveSpeed;
            this.rotSpeed = rotSpeed;
            this.lifeExpectancy = lifeExpectancy;
            enemyObj = Resources.Load("Prefabs/Enemy/"+this.name) as GameObject;
        }
    }
}