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
    public GameObject mainMenu;
    
    public GameObject gameOverScreen;
    public GameObject enemySpawner;
    public GameObject coinSpawner;
    public GameObject turtle;
    
    public GameObject magnetPowerUp;
    public GameObject bubblePowerUp;
    

    public void AddScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        score.text = playerScore.ToString();
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        enemySpawner.SetActive(true);
        coinSpawner.SetActive(true);
        turtle.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        enemySpawner.SetActive(true);
        coinSpawner.SetActive(true);
        turtle.SetActive(true);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        enemySpawner.SetActive(false);
        coinSpawner.SetActive(false);
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
        Debug.LogWarning("Started invincibility timer");
        bubblePowerUp.SetActive(true);
        var bubbleLoader = bubblePowerUp.transform.Find("BubbleLoader").gameObject;
        if (!bubbleLoader)
            return;
        var bubbleLoaderScript = bubbleLoader.GetComponent<BubbleLoaderScript>();
        bubbleLoaderScript.StartTimer();

    }
    
}
