using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CircleLoaderScript : MonoBehaviour
{
    public float duration = 3f; // Duration of the power-up timer
    private Image timerCircle;

    void Start()
    {
        // Set the position of the parent GameObject
        // transform.parent.position = new Vector3(38f, -18f, 0f);
        timerCircle = GetComponent<Image>(); // Assume this script is attached to the child GameObject with the Image
        if (timerCircle != null)
        {
            timerCircle.fillAmount = 1f;
        }

        // StartTimer();
    }

    public void StartTimer()
    {
        StartCoroutine(RunTimer());
    }

    private IEnumerator RunTimer()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            timerCircle.fillAmount = 1f - (elapsed / duration);
            yield return null;
        }

        transform.parent.gameObject.SetActive(false);
        // When the timer is finished, you can hide or destroy the parent UI element
        // Destroy(transform.parent.gameObject);
    }
}
