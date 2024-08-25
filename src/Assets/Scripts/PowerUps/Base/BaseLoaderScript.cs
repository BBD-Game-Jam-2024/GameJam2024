using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PowerUps.Base
{
    public class BaseLoaderScript : MonoBehaviour
    {
        [SerializeField] public float duration = 15f;
        private Image _timerCircle;

        private void OnEnable()
        {
            _timerCircle = GetComponent<Image>();
        }

        public void Start()
        {
            _timerCircle.fillAmount = 1f;
        }

        public void StartTimer()
        {
            Debug.LogWarning("Starting loader");
            StartCoroutine(RunTimer());
        }

        private IEnumerator RunTimer()
        {
            var elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                _timerCircle.fillAmount = 1f - elapsed / duration;
                yield return null;
            }

            transform.parent.gameObject.SetActive(false);
        }
    }
}