using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonCanvasToggle : MonoBehaviour
{
    public GameObject ConfCanvas;
    public GameObject FriCanvas;

    //隠しコマンド入力待ちフラグ
    bool konami = false;
    int commandNum = 0;

    void Start()
    {
        
    }

    void Update()
    {
        //隠しコマンド
        if (konami)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && commandNum == 0)
            {
                commandNum = 1;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && commandNum == 1)
            {
                commandNum = 2;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && commandNum == 2)
            {
                commandNum = 3;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && commandNum == 3)
            {
                commandNum = 4;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && commandNum == 4)
            {
                commandNum = 5;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && commandNum == 5)
            {
                commandNum = 6;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && commandNum == 6)
            {
                commandNum = 7;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && commandNum == 7)
            {
                commandNum = 8;
            }
            else if (Input.GetKeyDown(KeyCode.B) && commandNum == 8)
            {
                commandNum = 9;
            }
            else if (Input.GetKeyDown(KeyCode.A) && commandNum == 9)
            {
                //トロフィーの達成
                Debug.Log("command success!");
            }
        }
    }

    void FixedUpdate()
    {
        
    }

    public void Onclick()
    {
        if (gameObject.name == "ConfigurationBtn")
        {
            ConfCanvas.SetActive(true);
            konami = true;

        }
        else if (gameObject.name == "FriendBtn")
        {
            FriCanvas.SetActive(true);
        }
        else
        {
            ConfCanvas.SetActive(false);
            FriCanvas.SetActive(false);
            konami = false;
            commandNum = 0;
        }
    }
}
