using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    int score;
    float timer,sumTime;

    void Start()
    {
        score=0;
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
    }

    public void AddScore(int n)
    {
        score+=n;
    }

    public int GetScore()
    {
        return score;
    }

    public float[] GetTime()
    {
        sumTime += timer;
        float[] time = { timer, sumTime };
        timer = 0;
        return time;
    }

    public void resetScore()
    {
        score=0;
    }

    public void DoubleScore()
    {
        score*=(selectDifficulty.difficulty+1);
    }
}
