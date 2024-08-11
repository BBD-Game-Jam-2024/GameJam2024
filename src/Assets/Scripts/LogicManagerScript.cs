using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicManagerScript : MonoBehaviour
{
    public int playerScore;
    public Text score;
    public GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [ContextMenu("test")]
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
    
    
}
