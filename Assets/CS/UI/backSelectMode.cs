using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backSelectMode : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject modeCanvas1;
    public GameObject modeCanvas2;
    public GameObject buttonCanvas;
    public GameObject BG;
    Animator animator,animator2,animator3;

    //SE関係
    public AudioClip[] audioSEClips;

    void Start()
    {
        animator=menuCanvas.GetComponent<Animator>();
        animator3=buttonCanvas.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        
    }

    public void OnClick()
    {
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);

        BG.gameObject.GetComponent<Image>().color=new Color(0.471f,0.471f,0f,0.471f);
        if(modeCanvas1.transform.position.x<modeCanvas2.transform.position.x){
            animator2=modeCanvas1.GetComponent<Animator>();
        }
        else{
            animator2=modeCanvas2.GetComponent<Animator>();
        }
        animator.Play("MenuMove2");
        animator2.Play("ModeMove2");
        animator3.Play("ButtonMove2");
    }
}
