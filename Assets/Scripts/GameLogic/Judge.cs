﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Judge : MonoBehaviour
{
    public float preCountDown = 3f;
    public float countDown = 60f;

    // UI elements
    public Text timerLabel;
    public Text scoreLabel;

    public AudioClip fruitSoundEffect;

    // count down timer
    private float timer;
    private bool started;
    private bool finished;

    private static int highScore;
    private static int score;

    private GameFlowController gameFlowController;
    // Start is called before the first frame update
    void Start()
    {
        timer = preCountDown;
        started = false;
        finished = false;
        score = 0;
        highScore = PlayerPrefs.GetInt("high_score");

        if (!timerLabel)
        {
            Debug.LogError("No UI timer label!");
        }
        timerLabel.color = Color.yellow;
        if (!scoreLabel)
        {
            Debug.LogError("No score label!");
        }
        
        gameFlowController = EventSystem.current.gameObject.GetComponent<GameFlowController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                if (started) // finish
                {
                    finished = true;
                    FinishRound();
                }
                else // start
                {
                    started = true;
                    StartRound();
                }
            }
        }

        UpdateUI();
    }

    void StartRound()
    {
        Debug.Log("Round started!");
        timer = countDown;
        started = true;
        timerLabel.color = Color.green;
    }

    void FinishRound()
    {
        Debug.Log("Round finished!");
        finished = true;
        
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("high_score", highScore);        
        }
        PlayerPrefs.SetInt("score", score);

        if (gameFlowController)
        {
            // Switch to end scene
            gameFlowController.GoToScene(2);
        }
    }

    void UpdateUI()
    {
        timerLabel.text = Mathf.CeilToInt(timer).ToString();
    }

    public bool CanRepairFruit(GameObject FruitPiece1, GameObject FruitPiece2)
    {
        // Disabling this rule for now
        if (!FruitPiece1.GetComponent<FruitBase>().IsGrabbed()
            || !FruitPiece2.GetComponent<FruitBase>().IsGrabbed())
        {
            Debug.Log("At least one of the piece must be grabbed!");
            return false;
        }

        if (FruitPiece1.GetComponent<FruitBase>().fruitType != FruitPiece2.GetComponent<FruitBase>().fruitType)
        {
            Debug.Log("You can't combine two different fruit pieces!");
            return false;
        }

        if (FruitPiece1.GetComponent<FruitBase>().fruitPiece == 0
            || FruitPiece2.GetComponent<FruitBase>().fruitPiece == 0)
        {
            Debug.Log("Fruit is already in one piece!");
            return false;
        }

        if (FruitPiece1.GetComponent<FruitBase>().fruitPiece
            == FruitPiece2.GetComponent<FruitBase>().fruitPiece)
        {
            Debug.Log("You can't comebine same side!");
            return false;
        }

        return true;
    }

    public bool RepairFruit(GameObject FruitPiece1, GameObject FruitPiece2)
    {        
        // Get fruit type and generate corresponding type
        PieceGenerator pieceGenerator = gameObject.GetComponent<PieceGenerator>();
        if (pieceGenerator == null)
        {
            Debug.LogError("PieceGenerator should be under the same object as judge!");
            return false;
        }
        FruitBase.FruitType fruitType = FruitPiece1.GetComponent<FruitBase>().fruitType;
        GameObject fullFruit = GameObject.Instantiate(pieceGenerator.fullFruitList[(int) fruitType]);
        fullFruit.transform.position =
            (FruitPiece1.transform.position + FruitPiece2.transform.position) / 2;
        fullFruit.GetComponent<SimpleGravity>().velocity =
            (FruitPiece1.GetComponent<SimpleGravity>().velocity + FruitPiece2.GetComponent<SimpleGravity>().velocity) / 2;

        // Destroy fruit pieces
        pieceGenerator.ActiveFruitList.Remove(FruitPiece1);
        pieceGenerator.ActiveFruitList.Remove(FruitPiece2);
        Destroy(FruitPiece1);
        Destroy(FruitPiece2);

        gameObject.GetComponent<AudioSource>().PlayOneShot(fruitSoundEffect, 1.0f);

        score += 5;
        scoreLabel.text = "Score: " + score;
        return true;
    }
}
