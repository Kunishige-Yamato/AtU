using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    int score;

    void Start()
    {
        score=0;
    }

    void Update()
    {
        
    }

    public void AddScore(int n)
    {
        score+=n;
    }

    public int returnScore()
    {
        return score;
    }
}
