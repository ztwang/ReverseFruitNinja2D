using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Judge : MonoBehaviour
{
    public float preCountDown = 3f;
    public float countDown = 60f;

    public Text timerLabel;

    // count down timer
    private float timer;
    private bool started;
    private bool finished;

    private GameFlowController gameFlowController;
    // Start is called before the first frame update
    void Start()
    {
        timer = preCountDown;
        started = false;
        finished = false;
        if (!timerLabel)
        {
            Debug.LogError("No UI timer label!");
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

        updateUI();
    }

    void StartRound()
    {
        Debug.Log("Round started!");
        timer = countDown;
        started = true;
    }

    void FinishRound()
    {
        Debug.Log("Round finished!");
        finished = true;
        
        if (gameFlowController)
        {
            gameFlowController.GoToScene(2);
        }
    }

    void updateUI()
    {
        if (timerLabel)
        {
            timerLabel.text = Mathf.CeilToInt(timer).ToString();
        }
    }
}
