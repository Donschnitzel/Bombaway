﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIHandler : Singleton<UIHandler>
{
    [SerializeField] string StartSceneName = "StartScene";
    [SerializeField]
    RectTransform goblinUIParent;
    [SerializeField]
    GameObject goblinUIPrefab;

    [SerializeField]
    GameObject scoreSection;
    [SerializeField]
    TextMeshProUGUI scoreGnomes, scoreBombs, score, scoreTime;

    private void Start()
    {
        BombsAndGoblinsTracker.Instance.OutOfBombs += OnEnd;
        BombsAndGoblinsTracker.Instance.CollectedAllGoblins += OnEnd;
        BombsAndGoblinsTracker.Instance.GoblinAdded += OnGoblinAdded;

        gameObject.SetActive(true);
        scoreSection.SetActive(false);
        UpdateGoblinUI(0, BombsAndGoblinsTracker.Instance.TotalGoblins);
    }

    private void OnGoblinAdded()
    {
        UpdateGoblinUI(BombsAndGoblinsTracker.Instance.CollectedGoblins, BombsAndGoblinsTracker.Instance.TotalGoblins);
    }

    private void OnEnd()
    {
        var bombs = GameObject.FindObjectsOfType<Bomb>();

        ShowFinalScore(BombsAndGoblinsTracker.Instance.CollectedGoblins, BombsAndGoblinsTracker.Instance.TotalGoblins, BombsAndGoblinsTracker.Instance.TotalBomBs, bombs.Length, Score.Instance.Amount);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMenue();
        }
    }

    void UpdateGoblinUI(int found, int max)
    {
        for (int i = goblinUIParent.childCount - 1; i >= 0; i--)
        {
            Transform child = goblinUIParent.GetChild(i);
            if (child != null)
                Destroy(child.gameObject);
        }

        for (int i = 0; i < max; i++)
        {
            GameObject goblin = Instantiate(goblinUIPrefab, goblinUIParent);
            goblin.transform.localPosition = (Vector3.right * 60 * i);

            if (i >= found)
            {
                Image image = goblin.GetComponent<Image>();
                if (image != null)
                {
                    image.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                }
            }
        }
    }

    public void ShowFinalScore(int gnomesCollected, int gnomesMax, int bombsMax, int bombsLeft, int scoreFinal)
    {
        scoreFinal += Mathf.RoundToInt(60 - Time.timeSinceLevelLoad);
        scoreFinal = scoreFinal < 0 ? 0 : scoreFinal;
        scoreFinal = Mathf.RoundToInt(scoreFinal * 0.1f) * 10;
        scoreSection.SetActive(true);
        scoreGnomes.text = gnomesCollected + " / " + gnomesMax + " Gnomes saved. Points:" + (gnomesCollected*200-(gnomesMax-gnomesCollected)*-500);
        scoreBombs.text = bombsLeft + " / " + bombsMax + " Bombs used. Points: " + (bombsMax-bombsLeft)*100;
        scoreTime.text = "Timebonus " + Mathf.RoundToInt(60 - Time.timeSinceLevelLoad).ToString();
        score.text = scoreFinal.ToString();
    }

    public void ReturnToMenue()
    {
        SceneManager.LoadScene(StartSceneName);
    }

    public void NextScene()
    {
        Debug.Log("TRYING TO LOAD NEXT SCENE");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
