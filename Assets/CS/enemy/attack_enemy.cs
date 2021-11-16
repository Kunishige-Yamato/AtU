using System.Collections;
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

        }
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