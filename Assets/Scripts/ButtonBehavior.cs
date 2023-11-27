using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    public void OnClickRestart()
    {
        FruitsBehaviors.gameOver = false;
        Time.timeScale = 1.0f;
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
