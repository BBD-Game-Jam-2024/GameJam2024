using System.Collections;
using UnityEngine;

namespace PowerUps.Magnet
{
    public class MagnetPowerUpScript : MonoBehaviour
    {
        private const float DefaultRadius = 0.065f;
        private bool _isActivated;

        public LogicManagerScript logic;

        private void Start()
        {
            logic = GameObject
                .FindWithTag("Logic")
                .GetComponent<LogicManagerScript>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("CoinCollision") || _isActivated) return;

            _isActivated = true;
            var playerCollider = collision.gameObject.GetComponent<CircleCollider2D>();

            if (playerCollider)
            {
                Debug.LogWarning("Radius changed to 0.3");
                playerCollider.radius = 0.3f;
                StartCoroutine(RevertRadiusAfterDelay(playerCollider, 15f));
                MakeInvisible();
            }

            logic.StartMagnetTimer();
        }

        private void MakeInvisible()
        {
            var rendererComponent = GetComponent<Renderer>();
            if (rendererComponent != null)
            {
                rendererComponent.enabled = false;
            }

            var collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }

        private IEnumerator RevertRadiusAfterDelay(CircleCollider2D playerCollider, float delay,
            float originalRadius = DefaultRadius)
        {
            Debug.LogWarning("Going back to normal radius after 15s");
            yield return new WaitForSeconds(delay);
            Debug.LogWarning("It's been 15s");
            playerCollider.radius = originalRadius;
            Destroy(gameObject);
        }
    }
}