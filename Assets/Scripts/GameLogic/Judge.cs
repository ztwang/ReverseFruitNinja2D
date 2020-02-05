using System.Collections;
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
    private int timerNumber;

    public AudioClip fruitSoundEffect;

    // count down timer
    private float timer;
    private bool started;
    private bool isHalfway;
    private bool finished;

    private static int highScore;
    private static int score;

    private GameFlowController gameFlowController;
    private PieceGenerator pieceGenerator;

    // Start is called before the first frame update
    void Start()
    {
        timer = preCountDown;
        started = false;
        isHalfway = false;
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
        pieceGenerator = gameObject.GetComponent<PieceGenerator>();
        if (pieceGenerator == null)
        {
            Debug.LogError("PieceGenerator is missing in judge!");
        }
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
            else if (started && !isHalfway && timer <= (countDown / 2.0f))
            {
                // Only do once
                // Speed up half way of the game.
                isHalfway = true;
                pieceGenerator.generateInterval = pieceGenerator.generateInterval/ 2.0f;
                gameObject.GetComponent<AudioSource>().pitch += 0.3f;
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
        pieceGenerator.StartGenerate();
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
        if (timerNumber != Mathf.CeilToInt(timer))
        {
            timerNumber = Mathf.CeilToInt(timer);
            timerLabel.text = timerNumber.ToString();
            if (started && timerNumber <= 10)
            {
                timerLabel.GetComponent<UIHeartBeatAnimation>().Animate();
            }
        }
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

        if (FruitPiece1.GetComponent<FruitBase>().fruitType != FruitPiece2.GetComponent<FruitBase>().fruitType &&
            !(
            FruitPiece1.GetComponent<FruitBase>().fruitType == FruitBase.FruitType.Apple &&
            FruitPiece2.GetComponent<FruitBase>().fruitType == FruitBase.FruitType.Pen ||
            FruitPiece1.GetComponent<FruitBase>().fruitType == FruitBase.FruitType.Pen &&
            FruitPiece2.GetComponent<FruitBase>().fruitType == FruitBase.FruitType.Apple)
            )
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
        FruitBase.FruitType fruitType = FruitPiece1.GetComponent<FruitBase>().fruitType;

        // Apple Pen Apple logic
        if (FruitPiece1.GetComponent<FruitBase>().fruitType == FruitBase.FruitType.Apple &&
            FruitPiece2.GetComponent<FruitBase>().fruitType == FruitBase.FruitType.Pen ||
            FruitPiece1.GetComponent<FruitBase>().fruitType == FruitBase.FruitType.Pen &&
            FruitPiece2.GetComponent<FruitBase>().fruitType == FruitBase.FruitType.Apple)
        {
            if (FruitPiece1.GetComponent<FruitBase>().fruitPiece == FruitBase.FruitPiece.Left &&
                FruitPiece1.GetComponent<FruitBase>().fruitType == FruitBase.FruitType.Pen ||
                FruitPiece2.GetComponent<FruitBase>().fruitPiece == FruitBase.FruitPiece.Left &&
                FruitPiece2.GetComponent<FruitBase>().fruitType == FruitBase.FruitType.Pen)
            {
                fruitType = FruitBase.FruitType.PenApple;
            } else
            {
                fruitType = FruitBase.FruitType.ApplePen;
            }
        }
        // End
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

        ComboManager comboManager = GetComponent<ComboManager>();
        if (comboManager)
        {
            comboManager.Increment();
        }

        score += 5;
        scoreLabel.text = "Score: " + score;
        return true;
    }
}
