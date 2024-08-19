using System.Collections;
using System.Collections.Generic;
using TMPro;
using Turtle;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicManagerScript : MonoBehaviour
{
    public int playerScore;
    public Text score;
    public GameObject gameOverScreen;
    public GameObject magnetPowerUp;
    public GameObject invincibilityPowerUp;
    

    public void AddScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        score.text = playerScore.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        var turtle = GameObject.FindWithTag("Player");
        if (turtle != null)
            turtle.GetComponent<TurtleScript>().enabled = false;
    }

    public void StartMagnetTimer()
    {
        magnetPowerUp.SetActive(true);
        var magnetLoader = magnetPowerUp.transform.Find("MagnetLoader").gameObject;
        if (!magnetLoader)
            return;
        var magnetLoaderScript = magnetLoader.GetComponent<MagnetLoaderScript>();
        magnetLoaderScript.StartTimer();
        

    }

    public void StartInvincibilityTimer()
    {
        invincibilityPowerUp.SetActive(true);
        var invincibilityLoader = invincibilityPowerUp.Find("InvincibilityLoader").gameObject;
        if (!invincibilityLoader)
            return;
        var invincibilityLoaderScript = magnetLoader.GetComponent<InvincibilityLoaderScript>();
        invincibilityLoaderScript.StartTimer();

    }
    
}
