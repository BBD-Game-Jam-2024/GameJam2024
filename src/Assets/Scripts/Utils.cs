using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Random = System.Random;

public static class Utils
{
    public const string URL =
        "https://rjprox.pydevp.workers.dev?destination=https://turtletrouble.ryanjb.workers.dev/api/score";

    public static T RandomElementByWeight<T>(IEnumerable<T> sequence, Func<T, int, float> weightSelector)
    {
        var enumerable = sequence.ToList();
        var totalWeight = enumerable.Select(weightSelector).Sum();
        var itemWeightIndex = (float)(new Random().NextDouble() * totalWeight);
        float currentWeightIndex = 0;

        foreach (var item in enumerable.Select((item, index) =>
                     new { Value = item, Weight = weightSelector(item, index) }))
        {
            currentWeightIndex += item.Weight;
            if (currentWeightIndex >= itemWeightIndex)
                return item.Value;
        }

        return default;
    }

    [Serializable]
    public class ScoreList
    {
        public List<ScoreEntry> scoreEntries;

        public ScoreList(List<ScoreEntry> scoreEntries)
        {
            this.scoreEntries = scoreEntries;
        }
    }

    [Serializable]
    public class ScoreEntry
    {
        public string name;
        public int score;
        public ScoreEntry(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }

    public static IEnumerator HandleScoreTop(Action<ScoreList> processData)
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
}