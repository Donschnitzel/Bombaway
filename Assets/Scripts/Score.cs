using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : Singleton<Score>
{
    int currentScore = 0;

    //Negative Score will still be displayed as 0
    public int Amount { get => currentScore < 0 ? 0 : currentScore; }


    public void Add(int amount)
    {
        currentScore += amount;

    }



}
