using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    GameObject player;
    player pl;
    public Text debugText;

    void Start()
    {
        player=GameObject.Find("Player");
        pl=player.GetComponent<player>();
    }

    void FixedUpdate()
    {
        debugText.text="x:"+player.transform.position.x+" y:"+player.transform.position.y+" mx:"+pl.screenToWorldPointPosition.x+" my:"+pl.screenToWorldPointPosition.y;
    }
}
