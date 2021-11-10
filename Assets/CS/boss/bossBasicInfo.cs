using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossBasicInfo : MonoBehaviour
{
    //スキン
    SpriteRenderer skinSpriteRenderer;
    Sprite skinSprite;

    //体力
    int hp;
    int hit;

    //ボーナス
    int hitBonus;
    int defeatBonus;
    int timeBonus;

    //UI
    Slider hpBar;

    void Start()
    {
        //スキン設定
        skinSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        skinSpriteRenderer.sprite = skinSprite;

        //hpバー設定
        hpBar = GameObject.Find("Slider").GetComponent<Slider>();
        hpBar.maxValue = hp;
        hpBar.value = hpBar.maxValue;
    }

    void Update()
    {
        
    }

    //設定読み込み
    public void SetBasicInfo(string name,int hp, int hitBonus, int defeatBonus, int timeBonus)
    {
        this.hp = hp;
        this.hitBonus = hitBonus;
        this.defeatBonus = defeatBonus;
        this.timeBonus = timeBonus;
        skinSprite = Resources.Load<Sprite>("Textures/"+name);
    }

    //エフェクト再生用
    void EffectAdd(float x, float y, string name)
    {
        GameObject explosionPrefab = Resources.Load("Prefabs/Effect/" + name) as GameObject;
        Vector3 pos = new Vector3(x, y, 0);
        GameObject canvas = GameObject.Find("EffectCanvas");
        GameObject g = Instantiate(explosionPrefab, pos, Quaternion.identity);
        g.transform.SetParent(canvas.transform, false);
        g.transform.position = pos;
    }

    //被弾時
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            hit++;

            hpBar.value = hp - hit;

            //スコア付与
            GameObject scoreCounter = GameObject.Find("ScoreCounter");
            ScoreCount sc = scoreCounter.GetComponent<ScoreCount>();
            sc.AddScore(10 + 20 * (selectDifficulty.difficulty + 1));

            EffectAdd(col.transform.position.x,col.transform.position.y,"hitEffect");
            Destroy(col.gameObject);

            if (hit > hp)
            {
                //爆発
                EffectAdd(transform.position.x, transform.position.y, "defeatEffect");
                //早期撃退ボーナス
                //sc.AddScore((int)Mathf.Floor(10000*(1+selectDifficulty.difficulty)/timer));
                //次のステージへ
                /*if (selectDifficulty.endless)
                {
                    enemyGenerator2.DisplayResult();
                }
                else
                {
                    enemyGenerator.DisplayResult();
                }*/
                Destroy(gameObject);
            }
        }
    }
}
