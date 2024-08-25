using System.Linq;
using TMPro;
using UnityEngine;

public class LeaderboardScript : MonoBehaviour
{
    public GameObject textPrefab;
    public GameObject textBg;

    public void Populate()
    {
        StartCoroutine(Utils.HandleScoreTop(data =>
            {
                Debug.Log(data.scoreEntries.ToString());
                data.scoreEntries
                    .Select((scoreEntry, index) => (scoreEntry, index))
                    .ToList()
                    .ForEach(tuple => PopulateText(tuple.scoreEntry, tuple.index));
            })
        );
    }

    private void PopulateText(Utils.ScoreEntry scoreEntry, int index)
    {
        var textBgObj = Instantiate(textBg, gameObject.transform);
        var textObj = Instantiate(textPrefab, gameObject.transform);
        textBgObj.SetActive(true);
        textObj.SetActive(true);
        if (textObj.TryGetComponent<TMP_Text>(out var textMeshPro))
            textMeshPro.text = $"{index + 1}. {scoreEntry.name,14}  {scoreEntry.score,-10}";
        textObj.transform.localPosition = new Vector3(0, index * -50 + 80, 0);
        textBgObj.transform.localPosition = new Vector3(0, index * -50 + 80, 0);
    }
}