using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyBasicInfo : MonoBehaviour
{
    //スキン
    SpriteRenderer skinSpriteRenderer;
    public Sprite[] skinSprite;

    //体力
    int hp;
    int hit;

    //ボーナス
    int hitBonus;
    int defeatBonus;

    //移動
    float fallSpeed;
    float moveSpeed;
    float rotSpeed;

    //タイマー
    float timer;
    float lifeExpectancy;

    //hp0の時に消去したくない場合falseに
    bool defeatAndDestroy=false;

    //設定読み込み
    public void SetBasicInfo(string name, int hp, int hitBonus, int defeatBonus, float fallSpeed, float moveSpeed, float rotSpeed, float lifeExpectancy)
    {
        this.hp = hp;
        this.hitBonus = hitBonus;
        this.defeatBonus = defeatBonus;
        this.fallSpeed = fallSpeed;
        this.moveSpeed = moveSpeed;
        this.rotSpeed = rotSpeed;
        this.lifeExpectancy = lifeExpectancy;
        SetSkin(0);
    }

    //移動量教えるやつ
    public float[] GetSpeed()
    {
        float[] speed = { fallSpeed, moveSpeed, rotSpeed };
        return speed;
    }

    //移動量変化
    public void AddSpeed(float fallSpeed, float moveSpeed, float rotSpeed)
    {
        this.fallSpeed += fallSpeed;
        this.moveSpeed += moveSpeed;
        this.rotSpeed += rotSpeed;
    }

    //hp0時にdestroyしない設定に変更
    public void NotDestroy()
    {
        defeatAndDestroy = true;
    }

    void FixedUpdate()
    {
        //移動
        gameObject.transform.Translate(moveSpeed, -fallSpeed, 0, Space.World);
        transform.Rotate(0, 0, rotSpeed);

        //端まで行ったら消去
        if (transform.position.y < -10f || transform.position.y > 10f || transform.position.x < -10f || transform.position.x > 10f)
        {
            Destroy(gameObject);
        }

        //経過時間カウント
        timer += Time.deltaTime;

        //設定された時間で勝手に死ぬ
        if (timer >= lifeExpectancy)
        {
            Destroy(gameObject);
        }
    }

    //エフェクト再生用
    void EffectAdd(float x, float y, string name)
    {
        GameObject explosionPrefab = Resources.Load("Prefabs/Effect/" + name) as GameObject;
        Vector3 pos = new Vector3(x, y, 0);
        GameObject canvas = GameObject.Find("EffectCanvas");
        GameObject g = Instantiate(explosionPrefab, pos, Quaternion.identity);
        if(name=="defeatEffect")
        {
            g.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        }
        g.transform.SetParent(canvas.transform, false);
        g.transform.position = pos;
    }

    //当たったら消去
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            hit++;

            //スコア付与
            GameObject scoreCounter = GameObject.Find("ScoreCounter");
            ScoreCount sc = scoreCounter.GetComponent<ScoreCount>();
            sc.AddScore(hitBonus);

            EffectAdd(col.transform.position.x, col.transform.position.y, "hitEffect");
            Destroy(col.gameObject);

            if (hit >= hp)
            {
                //撃破ボーナス
                sc.AddScore(defeatBonus);
                if(defeatAndDestroy)
                {
                    attack_enemy atkEnemy = gameObject.GetComponent<attack_enemy>();
                    atkEnemy.defeatFlag = true;
                }
                else
                {
                    //爆発
                    EffectAdd(transform.position.x, transform.position.y, "defeatEffect");
                    Destroy(gameObject);
                }
            }
        }
    }

    public void SetSkin(int skinNum)
    {
        if (skinNum < skinSprite.Length && skinSprite[skinNum] != null) 
        {
            skinSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            skinSpriteRenderer.sprite = skinSprite[skinNum];
        }
        else
        {
            Debug.Log(skinNum+"番にはspriteが設定されていません");
        }
    }
}