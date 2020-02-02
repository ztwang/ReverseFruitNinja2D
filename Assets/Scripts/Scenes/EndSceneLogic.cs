using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneLogic : MonoBehaviour
{
    public Text endScoreLabel;
    public Text endHighScoreLabel;

    // Start is called before the first frame update
    void Start()
    {
        if (!endScoreLabel)
        {
            Debug.LogError("No score label in end scene!");
        }
        if (!endHighScoreLabel)
        {
            Debug.LogError("No high score label in end scene!");
        }
        int score = PlayerPrefs.GetInt("score");
        endScoreLabel.text = "Score: " + score;

        int highScore = PlayerPrefs.GetInt("high_score");
        endHighScoreLabel.text = "Best: " + highScore;
    }
}
