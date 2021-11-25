using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_boss : MonoBehaviour
{
    //選択された難易度
    protected int difficulty;

    //弾関係
    public GameObject[] bulPrefab;
    //弾射出元座標
    protected Vector3[] bulPos;

    //難易度，モード
    protected int[] modeDif;

    //ゲーム進行オブジェ
    GameObject progress;
    progress pro;

    //CSV関係
    TextAsset csvFile;
    List<string[]> csvDatas = new List<string[]>();

    //タイマー
    float timer;
    //攻撃一順時経過秒数保管用変数
    float lastTime;

    //攻撃クラス
    attack_boss_1 atk1;
    attack_boss_2 atk2;
    attack_boss_3 atk3;
    attack_boss_4 atk4;

    private void Start()
    {
        //進行用コンポーネント取得
        progress = GameObject.Find("Progress");
        pro = progress.GetComponent<progress>();

        //モード，難易度取得
        modeDif = pro.GetDifficulty();
        SetInfo(modeDif[0]);

        //タイマー初期化
        timer = 0;

        //csv読み込み
        csvFile = Resources.Load("CSV/" + name) as TextAsset;
        addList();

        //攻撃コンポーネント追加
        switch(gameObject.name)
        {
            case "boss1":
                atk1=gameObject.AddComponent<attack_boss_1>();
                atk1.bulPrefab = bulPrefab;
                atk1.bulPos = new Vector3[bulPrefab.Length];
                atk1.difficulty = difficulty;
                atk1.modeDif = modeDif;
                break;
            case "boss2":
                atk2=gameObject.AddComponent<attack_boss_2>();
                atk2.bulPrefab = bulPrefab;
                atk2.bulPos = new Vector3[bulPrefab.Length];
                atk2.difficulty = difficulty;
                atk2.modeDif = modeDif;
                break;
            case "boss3":
                atk3 = gameObject.AddComponent<attack_boss_3>();
                atk3.bulPrefab = bulPrefab;
                atk3.bulPos = new Vector3[bulPrefab.Length];
                atk3.difficulty = difficulty;
                atk3.modeDif = modeDif;
                break;
            case "boss4":
                atk4 = gameObject.AddComponent<attack_boss_4>();
                atk4.bulPrefab = bulPrefab;
                atk4.bulPos = new Vector3[bulPrefab.Length];
                atk4.difficulty = difficulty;
                atk4.modeDif = modeDif;
                break;
        }
    }

    void FixedUpdate()
    {
        //時間計測
        timer += Time.deltaTime;

        //CSVを監視して秒数ごとに攻撃
        for (int i = 0; i < csvDatas.Count; i++)
        {
            if (float.Parse(csvDatas[i][1]) + lastTime <= timer && csvDatas[i][2] == "0")
            {
                Generate(csvDatas[i][0]);
                if (csvDatas[i][0] != "end")
                {
                    csvDatas[i][2] = "1";
                }
                else
                {
                    lastTime += float.Parse(csvDatas[i][1]);
                }
            }
        }
    }

    public void SetInfo(int difficulty)
    {
        this.difficulty = difficulty;
    }

    //攻撃
    void Generate(string n)
    {
        //boss1: 1, 2, 3
        //boss2: 4, 5, 6, 7, 8
        //boss3: 9,10,11,12,13
        //boss4:14,15,16,17,18,19

        switch (n)
        {
            case "s1":
                atk1.Shoot1();
                break;
            case "s2":
                atk1.Shoot2();
                break;
            case "s3":
                atk1.Shoot3();
                break;
            case "s4":
                atk2.Shoot4();
                break;
            case "s5":
                atk2.Shoot5();
                break;
            case "s6":
                atk2.Shoot6();
                break;
            case "s7":
                atk2.Shoot7();
                break;
            case "s8":
                atk2.Shoot8();
                break;
            case "s9":
                atk3.Shoot9();
                break;
            case "s10":
                atk3.Shoot10();
                break;
            case "s11":
                atk3.Shoot11();
                break;
            case "s12":
                atk3.Shoot12();
                break;
            case "s13":
                atk3.Shoot13();
                break;
            case "s14":
                atk4.Shoot14();
                break;
            case "s15":
                atk4.Shoot15();
                break;
            case "s16":
                atk4.Shoot16();
                break;
            case "s17":
                atk4.Shoot17();
                break;
            case "s18":
                atk4.Shoot18();
                break;
            case "s19":
                atk4.Shoot19();
                break;
            case "end":
                addList();
                break;
        }
    }

    //CSVをリスト形式で格納
    void addList()
    {
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み，リストに追加
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
    }
}

public class attack_boss_1 : attack_boss
{
    //ビームの出す方向
    bool dir;
    //左右にビーム振るのに必要な変数
    float mov, maxMov, minMov;

    //弾名前用
    int num, num2;

    int countShoot3;

    void Start()
    {
        //変数初期化
        num = 0;
        num2 = 0;
        mov = -1;
        maxMov = mov * -1;
        minMov = mov;
        dir = false;
        countShoot3 = 0;
    }

    void FixedUpdate()
    {
        
    }

    public void Shoot1()
    {
        //弾射出元座標設定
        bulPos[0] = new Vector3(transform.position.x, transform.position.y - 2, 0);

        if (difficulty != 3)
        {
            for (float i = -2; i <= 2; i += 0.1f)
            {
                //弾生成，名付け
                GameObject mb = Instantiate(bulPrefab[0], bulPos[0], Quaternion.identity);
                mb.name = "bullet_" + num;
                num++;

                bullet8 b = mb.GetComponent<bullet8>();
                b.moveSpeed = i;
            }
        }
        else
        {
            for (float i = -2; i <= 2; i += 0.05f)
            {
                GameObject mb = Instantiate(bulPrefab[0], bulPos[0], Quaternion.identity);
                mb.name = "bullet_" + num;
                GameObject bul = GameObject.Find("bullet_" + num);
                num++;
                bullet8 b = bul.GetComponent<bullet8>();
                b.moveSpeed = i;
            }
        }
    }

    public void Shoot2()
    {
        //弾射出元座標設定
        bulPos[1] = new Vector3(transform.position.x, transform.position.y - 2, 0);

        if (mov <= maxMov && dir == false)
        {
            GameObject mb2 = Instantiate(bulPrefab[1], bulPos[1], Quaternion.identity);
            mb2.name = "bullet_" + num2;
            GameObject bul2 = GameObject.Find("bullet_" + num2);
            num2++;
            bullet9 b2 = bul2.GetComponent<bullet9>();
            b2.moveSpeed = mov / 10;
            mov += 0.03f;
            Invoke("Shoot2", 0.06f);
        }
        else if (dir == false)
        {
            Invoke("switchDir", 1);
        }
        if (mov >= minMov && dir == true)
        {
            GameObject mb2 = Instantiate(bulPrefab[1], bulPos[1], Quaternion.identity);
            mb2.name = "bullet_" + num2;
            GameObject bul2 = GameObject.Find("bullet_" + num2);
            num2++;
            bullet9 b2 = bul2.GetComponent<bullet9>();
            b2.moveSpeed = mov / 10;
            mov -= 0.03f;
            Invoke("Shoot2", 0.06f);
        }
        else if (dir == true)
        {
            Invoke("switchDir", 1);
        }
    }

    void switchDir()
    {
        if (dir == false)
        {
            dir = true;
            Shoot2();
        }
        else
        {
            dir = false;
        }
    }

    public void Shoot3()
    {
        //弾射出元座標設定
        bulPos[2].x = Random.Range(-8f, 8f);
        bulPos[2].y = 5.5f;
        Instantiate(bulPrefab[2], bulPos[2], Quaternion.identity);
        countShoot3++;

        if (countShoot3 < 50 && difficulty != 3)
        {
            Invoke("Shoot3", Random.Range(0.1f, 0.3f));
        }
        else if (countShoot3 < 100 && difficulty == 3)
        {
            Invoke("Shoot3", Random.Range(0.05f, 0.15f));
        }
        else
        {
            countShoot3 = 0;
        }
    }
}

public class attack_boss_2 : attack_boss
{
    int countShoot4;
    int countShoot6;
    int countShoot8;

    void Start()
    {
        //初期化
        countShoot4 = 0;
        countShoot6 = 0;
        countShoot8 = 0;
    }

    void FixedUpdate()
    {
        
    }

    public void Shoot4()
    {
        bulPos[0].x = Random.Range(-9, 9);
        bulPos[0].y = 5.5f;
        Instantiate(bulPrefab[0], bulPos[0], Quaternion.identity);
        if (countShoot4 < 80)
        {
            Invoke("Shoot4", 0.1f);
            countShoot4++;
        }
        else
        {
            countShoot4 = 0;
        }
    }

    public void Shoot5()
    {
        for (int i = -8; i <= 8; i += 2)
        {
            bulPos[1].x = i;
            bulPos[1].y = 5.5f;
            Instantiate(bulPrefab[1], bulPos[1], Quaternion.identity);
        }
    }

    public void Shoot6()
    {
        if (selectDifficulty.difficulty != 3)
        {
            for (float i = -6; i <= 2; i += 1.5f)
            {
                bulPos[2].x = -9.5f;
                bulPos[2].y = i;
                Instantiate(bulPrefab[2], bulPos[2], Quaternion.identity);
            }
        }
        else
        {
            for (float i = -9; i <= 5; i += 1.5f)
            {
                bulPos[2].x = -9.5f;
                bulPos[2].y = i;
                Instantiate(bulPrefab[2], bulPos[2], Quaternion.identity);
            }
        }
        if (countShoot6 < 2)
        {
            Invoke("Shoot6", 3.8f);
            countShoot6++;
        }
        else
        {
            countShoot6 = 0;
        }
    }

    public void Shoot7()
    {
        for (float i = -5.1f; i < 5; i += 2)
        {
            bulPos[3].x = -9.5f;
            bulPos[3].y = i;
            Instantiate(bulPrefab[3], bulPos[3], Quaternion.identity);
        }
        for (float i = -9f; i < 9; i += 2f)
        {
            bulPos[3].x = i;
            bulPos[3].y = 5.5f;
            Instantiate(bulPrefab[3], bulPos[3], Quaternion.identity);
        }
    }

    public void Shoot8()
    {
        bulPos[4].x = Random.Range(-8f, 8f);
        bulPos[4].y = -5.5f;
        Instantiate(bulPrefab[4], bulPos[4], Quaternion.identity);
        if (countShoot8 < 100 && selectDifficulty.difficulty != 3)
        {
            Invoke("Shoot8", 0.1f);
            countShoot8++;
        }
        else if (countShoot8 < 200 && selectDifficulty.difficulty == 3)
        {
            Invoke("Shoot8", 0.05f);
            countShoot8++;
        }
        else
        {
            countShoot8 = 0;
        }
    }
}


public class attack_boss_3 : attack_boss
{
    Vector3 objPlace;

    int mode;
    float moveSpeed, angle, posX;
    int countShoot11;
    int countShoot12;
    int countShoot13;

    void Start()
    {
        //初期化
        mode = 0;
        moveSpeed = 0;
        angle = 0;
        posX = -8;
        countShoot11 = 0;
        countShoot12 = 0;
        countShoot13 = 0;

        objPlace.x = 0;
        objPlace.y = 3;
        transform.position = objPlace;
    }

    void FixedUpdate()
    {

    }

    public void Shoot9()
    {
        bulPos[0].x = 0;

        //難易度クレイジーの時だけ2体出現
        if (difficulty==3)
        {
            bulPos[0].y = 5.8f;
            Instantiate(bulPrefab[0], bulPos[0], Quaternion.identity);
            bulPos[0].y = -5.8f;
            Instantiate(bulPrefab[0], bulPos[0], Quaternion.identity);
        }
        else
        {
            bulPos[0].y = 5.8f;
            Instantiate(bulPrefab[0], bulPos[0], Quaternion.identity);
        }
        Defense();
    }

    public void Shoot10()
    {
        if (mode == 0)
        {
            //ボス上移動
            moveSpeed = 0.01f;
            objPlace = transform.position;
            objPlace.y -= moveSpeed;
            transform.position = objPlace;
            if (transform.position.y <= 0)
            {
                moveSpeed = 0;
                objPlace.x = 0;
                objPlace.y = 0;
                transform.position = objPlace;
                mode = 1;
            }
        }
        if (mode == 1)
        {
            if (angle < 720)
            {
                float rad = Mathf.PI * (angle + 45) / 180;
                bulPos[1].x = (float)Mathf.Cos(rad) * 2 + transform.position.x;
                bulPos[1].y = (float)Mathf.Sin(rad) * 2 + transform.position.y;
                GameObject bul=Instantiate(bulPrefab[1], bulPos[1], Quaternion.identity);
                bul.transform.Rotate(0, 0, angle-90);
                if (selectDifficulty.difficulty != 0)
                {
                    rad = Mathf.PI * (angle + 225) / 180;
                    bulPos[1].x = (float)Mathf.Cos(rad) * 2 + transform.position.x;
                    bulPos[1].y = (float)Mathf.Sin(rad) * 2 + transform.position.y;
                    Instantiate(bulPrefab[1], bulPos[1], Quaternion.identity);
                }
                angle += 1.5f;
            }
            else
            {
                angle = 0;
                mode = 2;
            }
        }
        if (mode == 2)
        {
            //ボス移動
            moveSpeed = 0.01f;
            objPlace = transform.position;
            objPlace.y += moveSpeed;
            transform.position = objPlace;
            if (transform.position.y >= 3)
            {
                moveSpeed = 0;
                objPlace.x = 0;
                objPlace.y = 3;
                transform.position = objPlace;
                mode = 3;
            }
        }
        if (mode == 3)
        {
            mode = 0;
            Defense();

        }
        else
        {
            Invoke("Shoot10", 0.01f);
        }
    }

    public void Shoot11()
    {
        countShoot11++;
        for (float i = -5; i < 16; i += 2)
        {
            bulPos[2].y = i + i % 3;
            bulPos[2].x = -9.5f;
            Instantiate(bulPrefab[2], bulPos[2], Quaternion.identity);
            bulPos[2].x = 9.5f;
            Instantiate(bulPrefab[2], bulPos[2], Quaternion.identity);
        }
        if (countShoot11 < 5)
        {
            Invoke("Shoot11", 0.8f);
        }
        else
        {
            countShoot11 = 0;
        }
    }

    public void Shoot12()
    {
        countShoot12++;

        //上から
        bulPos[3].x = Random.Range(-9f, 9f);
        bulPos[3].y = 6f;
        Instantiate(bulPrefab[3], bulPos[3], Quaternion.identity);
        //右から
        bulPos[3].x = 9.5f;
        bulPos[3].y = Random.Range(-5f, 5f);
        Instantiate(bulPrefab[3], bulPos[3], Quaternion.identity);
        //左から
        bulPos[3].x = -9.5f;
        bulPos[3].y = Random.Range(-5f, 5f);
        Instantiate(bulPrefab[3], bulPos[3], Quaternion.identity);

        if (countShoot12 < 45)
        {
            Invoke("Shoot12", 0.2f);
        }
        else
        {
            countShoot12 = 0;
        }
    }

    public void Shoot13()
    {
        if (posX > 8)
        {
            posX = -8;
        }
        bulPos[4].x = posX;
        bulPos[4].y = 3;
        Instantiate(bulPrefab[4], bulPos[4], Quaternion.identity);

        countShoot13++;
        posX += 0.5f;

        //周回数
        int rap = 5;
        if (countShoot13 < 33 * rap)
        {
            Invoke("Shoot13", 0.05f);
        }
        else
        {
            countShoot13 = 0;
        }
    }

    void Defense()
    {
        for (float i = -1.5f; i <= 1.5f; i += 1.5f)
        {
            bulPos[5].x = transform.position.x + i;
            bulPos[5].y = transform.position.y - 2;
            Instantiate(bulPrefab[5], bulPos[5], Quaternion.identity);
        }
    }
}

public class attack_boss_4 : attack_boss
{
    int side;
    float angle;
    int hit,secForm;
    int countShoot15,countShoot17,countShoot18;

    public GameObject buddyPrefab;
    attack_boss_4 buddyClass;

    bossBasicInfo bossBasicInfo;
    int[] basicInfo;

    void Awake()
    {
        //必要情報取得
        bossBasicInfo = gameObject.GetComponent<bossBasicInfo>();
        basicInfo = bossBasicInfo.GetBasicInfo();
        secForm = basicInfo[0] / 2;

        //buddyがいない時は生成，いる時は自身の設定を行う
        buddyPrefab = GameObject.Find("boss4");
        if (buddyPrefab == gameObject)
        {
            SetBuddy();
        }
        else
        {
            buddyClass = buddyPrefab.GetComponent<attack_boss_4>();
            side = 1;
        }
    }

    void Start()
    {
        //初期化
        hit = 0;
        countShoot15 = 0;
        countShoot17 = 0;
        countShoot18 = 0;
    }

    void FixedUpdate()
    {

    }

    void SetBuddy()
    {
        gameObject.name = "boss4";
        side = -1;
        buddyPrefab = Resources.Load("Prefabs/Boss/boss4_buddy")as GameObject;
        GameObject buddy = Instantiate(buddyPrefab, new Vector2(transform.position.x * -1, transform.position.y), Quaternion.identity);
        buddyPrefab = buddy;
        buddy.transform.parent = gameObject.transform;

        bossBasicInfo buddyBasicInfo = buddy.GetComponent<bossBasicInfo>();
        buddyBasicInfo.SetBasicInfo(gameObject.name, basicInfo[0], basicInfo[1], basicInfo[2], basicInfo[3]);
    }

    public void Shoot14()
    {
        for (float i = -4f; i < 6; i += 3)
        {
            bulPos[0].x = 9.5f * side;
            bulPos[0].y = i;
            Instantiate(bulPrefab[0], bulPos[0], Quaternion.identity);
        }
    }

    public void Shoot15()
    {
        angle = Random.Range(-180f, 360f);
        float rad = Mathf.PI * angle / 180;
        bulPos[1].x = (float)Mathf.Cos(rad) * 2 + transform.position.x;
        bulPos[1].y = (float)Mathf.Sin(rad) * 2 + transform.position.y;
        Instantiate(bulPrefab[1], bulPos[1], Quaternion.identity);
        countShoot15++;
        if (countShoot15 < 150)
        {
            Invoke("Shoot15", 0.08f);
        }
        else
        {
            countShoot15 = 0;
        }
    }

    public void Shoot16()
    {
        if (side < 0)
        {
            bulPos[2].x = Random.Range(-4f, 0);
        }
        else
        {
            bulPos[2].x = Random.Range(0, 4f);
        }
        bulPos[2].y = 9f;
        GameObject go = Instantiate(bulPrefab[2], bulPos[2], Quaternion.identity) as GameObject;
        go.name = "bullet23-1(" + side + ")";
    }

    public void Shoot17()
    {
        if (transform.position.x > 0)
        {
            int i = Random.Range(-4, 5) * 2;
            for (int j = -8; j <= 8; j += 2)
            {
                bulPos[3].x = j;
                bulPos[3].y = 6f;
                if (i != j)
                {
                    Instantiate(bulPrefab[3], bulPos[3], Quaternion.identity);
                }
            }
            countShoot17++;
            if (countShoot17 < 6)
            {
                Invoke("Shoot17", 1.5f);
            }
            else
            {
                countShoot17 = 0;
            }
        }

    }

    public void Shoot18()
    {
        Instantiate(bulPrefab[4], transform.position, Quaternion.identity);
        countShoot18++;
        if (countShoot18 < 75)
        {
            Invoke("Shoot18", 0.15f);
        }
        else
        {
            countShoot18 = 0;
        }
    }

    public void Shoot19()
    {
        bulPos[5].y = 6;
        if (side > 0)
        {
            if (selectDifficulty.difficulty != 3)
            {
                for (int i = -8; i <= 8; i += 2)
                {
                    bulPos[5].x = i;
                    Instantiate(bulPrefab[5], bulPos[5], Quaternion.identity);
                }
            }
            else
            {
                for (int i = -8; i <= 8; i += 1)
                {
                    bulPos[5].x = i;
                    Instantiate(bulPrefab[5], bulPos[5], Quaternion.identity);
                }
            }
        }
    }

    //当たったら消去
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            hit++;

            //hard以上は被弾すると追尾弾ばら撒く
            if (modeDif[0] >= 2)
            {
                if (hit % 10 == 0)
                {
                    int ran = Random.Range(0, 3);
                    switch (ran)
                    {
                        case 0:
                            Instantiate(bulPrefab[6], transform.position, Quaternion.identity);
                            break;
                        case 1:
                            Instantiate(bulPrefab[7], transform.position, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(bulPrefab[8], transform.position, Quaternion.identity);
                            break;
                    }
                }
                //体力半分切ったら更に頻繁にばら撒く
                if (hit > secForm)
                {
                    if (hit % 10 == 5)
                    {
                        int ran2 = Random.Range(0, 3);
                        switch (ran2)
                        {
                            case 0:
                                Instantiate(bulPrefab[6], transform.position, Quaternion.identity);
                                break;
                            case 1:
                                Instantiate(bulPrefab[7], transform.position, Quaternion.identity);
                                break;
                            case 2:
                                Instantiate(bulPrefab[8], transform.position, Quaternion.identity);
                                break;
                        }
                    }
                }
                //クレイジーは体力1/4ですごいことになる
                if (hit > secForm / 2) 
                {
                    if(hit%10!=0&&hit%10!=5)
                    {
                        int ran2 = Random.Range(0, 3);
                        switch (ran2)
                        {
                            case 0:
                                Instantiate(bulPrefab[6], transform.position, Quaternion.identity);
                                break;
                            case 1:
                                Instantiate(bulPrefab[7], transform.position, Quaternion.identity);
                                break;
                            case 2:
                                Instantiate(bulPrefab[8], transform.position, Quaternion.identity);
                                break;
                        }
                    }
                }
            }

            bossBasicInfo.EqualHp(buddyPrefab);
        }
    }
}