using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enemyInfo
{
    //基底クラス
    [System.Serializable]
    public class Enemy
    {
        public int hp;
        public int hitBonus;
        public int defeatBonus;

        public Enemy(int hp,int hitBonus,int defeatBonus)
        {
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

        public Boss(string name, int hp, int hitBonus, int defeatBonus,int timeBonus) : base(hp, hitBonus, defeatBonus)
        {
            this.name = name;
            this.timeBonus = timeBonus;
        }

        public void PrintBossInfo()
        {
            Debug.Log(name + "," + hp + "," + hitBonus + "," + defeatBonus + "," + timeBonus);
        }
    }

    [System.Serializable]
    public class Player
    {
        public int hp;
        public int attack;
        public int defense;
    }
}