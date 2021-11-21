﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class attack_enemy : MonoBehaviour
{
    //弾関係
    public GameObject[] bulPrefab;
    //弾射出座標設定
    protected Vector2 bulPos;

    //タイマー
    public float timer;

    //敵攻撃用基底クラス
    protected attack_enemy atkClass;

    //撃破フラグ
    public bool defeatFlag; 

    void Start()
    {
        //敵種別番号取得
        var pattern = "([0-9]*$)";
        var myNumber = Regex.Match(gameObject.name, pattern);

        switch (myNumber.ToString())
        {
            case "1":
                attack_enemy_1 atk1 = gameObject.AddComponent<attack_enemy_1>();
                atk1.bulPrefab = bulPrefab;
                atk1.atkClass = gameObject.GetComponent<attack_enemy>();
                break;
            case "2":
                attack_enemy_2 atk2 = gameObject.AddComponent<attack_enemy_2>();
                atk2.bulPrefab = bulPrefab;
                atk2.atkClass = gameObject.GetComponent<attack_enemy>();
                break;
            case "3":
                attack_enemy_3 atk3 = gameObject.AddComponent<attack_enemy_3>();
                atk3.bulPrefab = bulPrefab;
                atk3.atkClass = gameObject.GetComponent<attack_enemy>();
                break;
            case "4":
                attack_enemy_4 atk4 = gameObject.AddComponent<attack_enemy_4>();
                atk4.bulPrefab = bulPrefab;
                atk4.atkClass = gameObject.GetComponent<attack_enemy>();
                break;
            case "5":
                attack_enemy_5 atk5 = gameObject.AddComponent<attack_enemy_5>();
                atk5.bulPrefab = bulPrefab;
                atk5.atkClass = gameObject.GetComponent<attack_enemy>();
                break;
            case "6":
                attack_enemy_6 atk6 = gameObject.AddComponent<attack_enemy_6>();
                atk6.bulPrefab = bulPrefab;
                atk6.atkClass = gameObject.GetComponent<attack_enemy>();
                break;
            case "7":
                attack_enemy_7 atk7 = gameObject.AddComponent<attack_enemy_7>();
                atk7.bulPrefab = bulPrefab;
                atk7.atkClass = gameObject.GetComponent<attack_enemy>();
                break;

        }

        defeatFlag = false;
    }

    void FixedUpdate()
    {
        //経過時間カウント
        timer += Time.deltaTime;
    }
}

public class attack_enemy_1 : attack_enemy
{
    //基礎情報コンポーネント
    enemyBasicInfo basicInfo;

    void Start()
    {
        if (gameObject.transform.position.x >= 0)
        {
            bulPos.x = gameObject.transform.position.x - 0.5f;
        }
        else
        {
            bulPos.x = gameObject.transform.position.x + 0.5f;
        }

        //基礎情報コンポーネント取得
        basicInfo = gameObject.GetComponent<enemyBasicInfo>();

        Shoot();
    }

    void FixedUpdate()
    {
        //加速
        basicInfo.AddSpeed(0.001f,0,0);
    }

    void Shoot()
    {
        Invoke("Shoot", 0.3f);
        Instantiate(bulPrefab[0], new Vector3( bulPos.x, transform.position.y, 0), Quaternion.identity);
    }
}

public class attack_enemy_2 : attack_enemy
{
    //基礎情報コンポーネント
    enemyBasicInfo basicInfo;

    //移動方向制御
    float direction = -1;

    //反転時のスピード
    float turningSpeed;

    void Start()
    {
        //弾射出座標設定
        bulPos.x = gameObject.transform.position.x;
        bulPos.y = gameObject.transform.position.y - 0.5f;

        //基礎情報コンポーネント取得
        basicInfo = gameObject.GetComponent<enemyBasicInfo>();

        //反転スピード設定
        turningSpeed = basicInfo.GetSpeed()[1] * -2;

        Shoot();
    }

    void FixedUpdate()
    {
        if (transform.position.x > 7 && direction == 1 || transform.position.x < -7 && direction == -1) 
        {
            ChangeDirection();
        }

        bulPos.x = transform.position.x;
    }

    //移動方向反転
    void ChangeDirection()
    {
        direction *= -1;
        basicInfo.AddSpeed(0, turningSpeed * direction, 0);
    }

    void Shoot()
    {
        Invoke("Shoot", 0.4f);
        Instantiate(bulPrefab[0], new Vector3(bulPos.x, transform.position.y, 0), Quaternion.identity);
    }
}

public class attack_enemy_3 : attack_enemy
{
    float[] bulAngle=new float[5];

    void Start()
    {
        bulPos.x=gameObject.transform.position.x;
        bulPos.y=gameObject.transform.position.y-1;

        Shoot();
    }

    void Shoot()
    {
        for (int i = 0; i < bulAngle.Length; i++)
        {
            //弾発射角度設定
            bulAngle[i] = -0.15f + 0.075f * i;

            //弾生成
            GameObject mb = Instantiate(bulPrefab[0], bulPos, Quaternion.identity);
            bullet2 b2 = mb.GetComponent<bullet2>();
            b2.moveSpeed = bulAngle[i];
        }

        Invoke("Shoot", 1.2f);
    }
}

public class attack_enemy_4 : attack_enemy
{
    float angle;

    void Start()
    {
        gameObject.name = "enemy4Prefab";

        Shoot();
    }

    void Shoot()
    {
        angle = Random.Range(-180f, 360f);
        float rad = Mathf.PI * angle / 180;
        bulPos.x = (float)Mathf.Cos(rad) * 1.5f + transform.position.x;
        bulPos.y = (float)Mathf.Sin(rad) * 1.5f + transform.position.y;
        GameObject bul = Instantiate(bulPrefab[0], bulPos, Quaternion.identity);
        bullet26 bulCom = bul.GetComponent<bullet26>();
        bulCom.enemyPrefab = gameObject;
        bulCom.parent = 4;
        Invoke("Shoot", 0.07f);
    }
}

public class attack_enemy_5 : attack_enemy
{
    float angle;
    bool defeat = false;
    Sprite image;
    Vector3 bulletScale;
    enemyBasicInfo basicInfo;

    void Start()
    {
        gameObject.name = "enemy5Prefab";
        image = Resources.Load("Textures/enemy5_2") as Sprite;
        progress pro = GameObject.Find("Progress").GetComponent<progress>();
        if (pro.GetDifficulty()[0] == 3 && pro.GetStageNum()[0] == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = image;
        }

        basicInfo = gameObject.GetComponent<enemyBasicInfo>();
        basicInfo.NotDestroy();
    }

    void FixedUpdate()
    {
        if (atkClass.defeatFlag && defeat == false)
        {
            Shoot();
            defeat = true;
        }
    }

    void Shoot()
    {
        for (int i = 0; i < 360; i += 15)
        {
            angle = i;
            float rad = Mathf.PI * angle / 180;
            bulPos.x = (float)Mathf.Cos(rad) * 0.5f + transform.position.x;
            bulPos.y = (float)Mathf.Sin(rad) * 0.5f + transform.position.y;
            GameObject bul = Instantiate(bulPrefab[0], bulPos, Quaternion.identity) as GameObject;
            bullet26 bulCom = bul.GetComponent<bullet26>();
            bulCom.enemyPrefab = gameObject;
            bulCom.parent = 5;
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //ブレーキ
        basicInfo.AddSpeed(-0.005f, 0, 0);
        //拡大
        bulletScale = transform.localScale;
        bulletScale.x += 0.08f;
        bulletScale.y += 0.08f;
        transform.localScale = bulletScale;
    }
}

public class attack_enemy_6 : attack_enemy
{
    float fallSpeed,angle;
    bool defeat=false;
    Vector3 bulletScale;
    Sprite image;

    void Start()
    {
        gameObject.name = "enemy6Prefab";
        image = Resources.Load("Textures/enemy6_2") as Sprite;
        progress pro = GameObject.Find("Progress").GetComponent<progress>();
        if (pro.GetDifficulty()[0] == 3 && pro.GetStageNum()[0] == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = image;
        }

        enemyBasicInfo basicInfo = gameObject.GetComponent<enemyBasicInfo>();
        basicInfo.NotDestroy();
    }

    void FixedUpdate()
    {
        if (atkClass.defeatFlag && defeat==false)
        {
            // CapselColliderの取得
            CircleCollider2D[] myCol = gameObject.GetComponents<CircleCollider2D>();
            // Colliderを無効化
            foreach (CircleCollider2D col in myCol)
            {
                col.enabled = false;
            }

            Shoot();
            defeat = true;
        }
    }

    void Shoot()
    {
        enemyBasicInfo basicInfo = gameObject.GetComponent<enemyBasicInfo>();
        fallSpeed = basicInfo.GetSpeed()[0];

        if (fallSpeed > 0)
        {
            //ブレーキ
            basicInfo.AddSpeed(-0.003f,0,0);
            //拡大
            bulletScale = transform.localScale;
            bulletScale.x += 0.009f;
            bulletScale.y += 0.006f;
            transform.localScale = bulletScale;
            Invoke("Shoot", 0.05f);
        }
        else
        {
            fallSpeed = 0;
            for (int i = 0; i < 360; i += 12)
            {
                angle = i;
                float rad = Mathf.PI * angle / 180;
                bulPos.x = (float)Mathf.Cos(rad) * 0.5f + transform.position.x;
                bulPos.y = (float)Mathf.Sin(rad) * 0.5f + transform.position.y;
                GameObject bul = Instantiate(bulPrefab[0], bulPos, Quaternion.identity);
                bullet26 bulCom = bul.GetComponent<bullet26>();
                bulCom.enemyPrefab = gameObject;
                bulCom.parent = 6;
                Destroy(gameObject);
            }
        }
    }
}

public class attack_enemy_7 : attack_enemy
{
    //回転移動に必要な変数
    float angle, radius, speed;
    Vector2 enemyPlace;

    void Start()
    {
        angle = Random.Range(30f, 150f);
        radius = Random.Range(3f, 6f);
        speed = Random.Range(1f, 3f);
        if (angle % 2 < 1)
        {
            speed *= -1;
        }
    }

    void FixedUpdate()
    {
        enemyPlace = transform.position;
        angle += speed;
        float rad = Mathf.PI * angle / 180;
        enemyPlace.x = (float)Mathf.Cos(rad) * radius + 0;
        enemyPlace.y = (float)Mathf.Sin(rad) * radius + 6;
        transform.position = enemyPlace;
    }
}