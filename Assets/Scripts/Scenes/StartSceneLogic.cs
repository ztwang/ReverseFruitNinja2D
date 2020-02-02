using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneLogic : MonoBehaviour
{
    public Text startHighScoreLabel;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!startHighScoreLabel)
        {
            Debug.LogError("No high score label in end scene!");
        }
        int highScore = PlayerPrefs.GetInt("high_score");
        startHighScoreLabel.text = "Best: " + highScore;
    }
}
