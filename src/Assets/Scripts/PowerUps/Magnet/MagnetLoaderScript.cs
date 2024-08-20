using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MagnetLoaderScript : MonoBehaviour
{
    public float duration = 15f;
    private Image _timerCircle;
    private bool _first = true;

    private void Start()
    {
        _timerCircle = GetComponent<Image>();
        if (_timerCircle != null)
        {
            _timerCircle.fillAmount = 1f;
        }
    }

    public void StartTimer()
    {
        Debug.LogWarning("Starting things");
        StartCoroutine(RunTimer());
    }

    private IEnumerator RunTimer()
    {
        if(!_first)
            _timerCircle.fillAmount = 1f;

        var elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (_timerCircle)
                _timerCircle.fillAmount = 1f - (elapsed / duration);
            yield return null;
        }

        transform.parent.gameObject.SetActive(false);
        _first = false;
    }
}
