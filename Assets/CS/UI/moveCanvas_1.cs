using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveCanvas_1 : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject modeCanvas1;
    public GameObject modeCanvas2;
    public GameObject buttonCanvas;
    public GameObject BG;
    Animator animator,animator2,animator3;

    void Start()
    {
        animator=menuCanvas.GetComponent<Animator>();
        if(gameObject.name=="StoryModeButton"){
            animator2=modeCanvas1.GetComponent<Animator>();
        }
        if(gameObject.name=="EndlessModeButton"){
            animator2=modeCanvas2.GetComponent<Animator>();
        }
        animator3=buttonCanvas.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        
    }

    public void OnClick()
    {
        if(gameObject.name=="StoryModeButton"){
            BG.gameObject.GetComponent<Image>().color=new Color(0f,0.471f,0f,0.471f);
        }
        if(gameObject.name=="EndlessModeButton"){
            BG.gameObject.GetComponent<Image>().color=new Color(0f,0f,0.471f,0.471f);
        }
        animator.Play("MenuMove1");
        animator2.Play("ModeMove1");
        animator3.Play("ButtonMove1");
    }
}
