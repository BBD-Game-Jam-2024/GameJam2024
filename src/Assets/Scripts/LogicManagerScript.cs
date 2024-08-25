using System.Collections;
using System.Text;
using PowerUps.Base;
using TMPro;
using Turtle;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = System.Random;

public class LogicManagerScript : MonoBehaviour
{
    [FormerlySerializedAs("playerScore")] public int turtleScore;
    public TMP_Text score;
    public GameObject mainMenu;

    public GameObject gameOverScreen;
    public GameObject leaderboardScreen;
    public GameObject leaderboardScreenOg;

    public GameObject turtle;

    public GameObject multiplierPowerUp;
    public GameObject bubblePowerUp;

    public GameObject panicBackgroundNotShowingOnWeb;
    public GameObject panicBackgroundNotShowingOnWebLeader;

    public GameObject spawner;

    public TMP_InputField turtleNameField;

    private string _turtleName;


    [FormerlySerializedAs("BackgroundAudioMenu")] [SerializeField]
    public GameObject backgroundAudioMenu;

    [FormerlySerializedAs("BackgroundAudioMain")] [SerializeField]
    public GameObject backgroundAudioMain;

    // Hack -> 
    private bool _isAdding;
    private int _multiplier = 1;

    public void AddScore(int scoreToAdd)
    {
        turtleScore += scoreToAdd * _multiplier;
        score.text = $"{_turtleName} Score: {turtleScore}";
    }

    public void StartGame()
    {
        score.gameObject.SetActive(true);
        mainMenu.SetActive(false);
        spawner.SetActive(true);
        panicBackgroundNotShowingOnWeb.SetActive(false);
        // Yes, bad should have figured out how to swap the track and not just disable/enable these.
        backgroundAudioMain.SetActive(true);
        backgroundAudioMenu.SetActive(false);
        turtle.SetActive(true);
        _turtleName = string.IsNullOrWhiteSpace(turtleNameField?.text)
            ? $"Turtle {new Random().Next(99)}"
            : turtleNameField.text;
        score.text = $"{_turtleName} Score: {turtleScore}";
    }

    public void ShowLeaderBoard()
    {
        mainMenu.SetActive(false);
        panicBackgroundNotShowingOnWeb.SetActive(false);
        leaderboardScreenOg.SetActive(true);
        leaderboardScreen.SetActive(true);
        panicBackgroundNotShowingOnWebLeader.SetActive(true);
        if (leaderboardScreen.TryGetComponent<LeaderboardScript>(out var sc)) sc.Populate();
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        turtle.SetActive(true);
    }

    public void GameOver()
    {
        spawner.SetActive(false);
        gameOverScreen.SetActive(true);
        turtle.GetComponent<TurtleScript>().enabled = false;
        if (turtle.gameObject.TryGetComponent<CircleCollider2D>(out var component)) component.radius = 0;
        StartCoroutine(HandleScorePost());
    }


    private IEnumerator HandleScorePost()
    {
        if (_isAdding) yield break;
        _isAdding = true;
        // Stuff Unity and its JSON converter doesn't work
        var jsonBody = $"{{\"name\":\"{_turtleName}\", \"score\":{turtleScore}}}";
        var bytes = Encoding.UTF8.GetBytes(jsonBody);
        using var request = new UnityWebRequest(Utils.URL, "POST");
        request.uploadHandler = new UploadHandlerRaw(bytes);
        request.SetRequestHeader("Content-Type", "application/json");
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();
        _isAdding = false;
        if (request.result == UnityWebRequest.Result.Success) yield break;

        Debug.LogError($"Error in sending request: {request.error}");
        Debug.LogError($"Response Code: {request.responseCode}");
        Debug.LogError($"Response: {request.downloadHandler.text}");
    }

    public void StartMultiplierTimer()
    {
        multiplierPowerUp.SetActive(true);
        var magnetLoader = multiplierPowerUp.transform.Find("MultiplierLoader").gameObject;
        if (!magnetLoader)
            return;
        var magnetLoaderScript = magnetLoader.GetComponent<BaseLoaderScript>();
        magnetLoaderScript.StartTimer();
        
    }

    /*public void StartMagnetTimer()
    {
        multiplierPowerUp.SetActive(true);
        var magnetLoader = multiplierPowerUp.transform.Find("MagnetLoader").gameObject;
        if (!magnetLoader)
            return;
        var magnetLoaderScript = magnetLoader.GetComponent<BaseLoaderScript>();
        magnetLoaderScript.StartTimer();
    }*/

    public void StartInvincibilityTimer()
    {
        Debug.LogWarning("Started invincibility timer from function");
        bubblePowerUp.SetActive(true);
        var bubbleLoader = bubblePowerUp.transform.Find("BubbleLoader").gameObject;
        if (!bubbleLoader)
            return;
        bubbleLoader.GetComponent<BaseLoaderScript>()?.StartTimer();
    }
}