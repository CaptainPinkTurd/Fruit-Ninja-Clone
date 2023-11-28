using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

public class ScoreScript : MonoBehaviour
{
    [Header("Score Element")]
    [SerializeField] TMP_Text scoreTextGame;
    [SerializeField] TMP_Text scoreTextMenu;
    [SerializeField] TMP_Text highScoreTextGame;
    [SerializeField] TMP_Text highScoreTextMenu;
    static int score;

    [Header("GameOver Element")]
    [SerializeField] GameObject gameOverPanel;

    private void Awake()
    {
        score = 0;  
        gameOverPanel.SetActive(false); 
        if(PlayerPrefs.GetInt("HighScore") == null)
        {
            PlayerPrefs.SetInt("HighScore", 0);
            highScoreTextGame.text = "BEST: " + PlayerPrefs.GetInt("HighScore").ToString();
        }
        else
        {
            highScoreTextGame.text = "BEST: " + PlayerPrefs.GetInt("HighScore").ToString();
        }
    }
    private void Update()
    {
        if (!FruitsBehaviors.gameOver)
        {
            scoreTextGame.text = score.ToString();
            scoreTextMenu.text = "Score: " + score.ToString();
            if(score >= PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", score);
                highScoreTextGame.text = "BEST: " + PlayerPrefs.GetInt("HighScore").ToString();
            }
        }
        else
        {
            highScoreTextMenu.text = "High: " + PlayerPrefs.GetInt("HighScore").ToString();
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    internal static void AddScore()
    {
        score++;
    }
}
