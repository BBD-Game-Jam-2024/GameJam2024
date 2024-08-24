using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Turtle;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicManagerScript : MonoBehaviour
{
    private const string URL = "https://turtletrouble.ryanjb.workers.dev/api/score";

    public int playerScore;
    public Text score;
    public GameObject mainMenu;

    public GameObject gameOverScreen;
    public GameObject mainMenuImg;
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
        // TODO: disable/kill all the current enemies
        StartCoroutine(HandleScorePost());

        // TODO: some kind of board 
        StartCoroutine(HandleScoreTop(Debug.Log));
        //TODO: why not use global turtle object.
        var turtle = GameObject.FindWithTag("Player");
        if (turtle != null)
            turtle.GetComponent<TurtleScript>().enabled = false;
    }

    private static IEnumerator HandleScoreTop(Action<ScoreList> processData)
    {
        var request = UnityWebRequest.Get(URL);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            var downloadHandlerText = request.downloadHandler.text;
            Debug.Log(downloadHandlerText);
            processData(JsonUtility.FromJson<ScoreList>(downloadHandlerText));
        }

        else
            Debug.LogError("GET request failed: " + request.error);
    }


    private IEnumerator HandleScorePost()
    {
        const string playerName = "cool and the gang";
        // Stuff Unity and its JSON converter doesn't work
        var jsonBody = $"{{\"name\":\"{playerName}\", \"score\":{playerScore}}}";
        var bytes = Encoding.UTF8.GetBytes(jsonBody);
        using var request = new UnityWebRequest(URL, "POST");
        request.uploadHandler = new UploadHandlerRaw(bytes);
        request.SetRequestHeader("Content-Type", "application/json");
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success) yield break;

        Debug.LogError("Error in sending request: " + request.error);
        Debug.LogError("Response Code: " + request.responseCode);
        Debug.LogError("Response: " + request.downloadHandler.text);
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
        bubblePowerUp.SetActive(true);
        var bubbleLoader = bubblePowerUp.transform.Find("BubbleLoader").gameObject;
        if (!bubbleLoader)
            return;
        var bubbleLoaderScript = bubbleLoader.GetComponent<BubbleLoaderScript>();
        bubbleLoaderScript.StartTimer();
    }
}

[Serializable]
public class ScoreList
{
    public List<ScoreEntry> ScoreEntries;

    public ScoreList(List<ScoreEntry> scoreEntries)
    {
        ScoreEntries = scoreEntries;
    }
}

[Serializable]
public class ScoreEntry
{
    public string Name { get; }
    public int Score { get; }

    public ScoreEntry(string name, int score)
    {
        Name = name;
        Score = score;
    }
}