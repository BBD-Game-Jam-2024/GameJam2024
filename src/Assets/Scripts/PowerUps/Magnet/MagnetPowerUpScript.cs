using System.Collections;
using UnityEngine;

namespace PowerUps.Magnet
{
    public class MagnetPowerUpScript : MonoBehaviour
    {
        private const float DefaultRadius = 0.2f;
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
            if (collision.gameObject.TryGetComponent<CircleCollider2D>(out var playerCollider))
            {
                playerCollider.radius = 0.5f;
                StartCoroutine(RevertRadiusAfterDelay(playerCollider, 15f));
                MakeInvisible();
            }

            logic.StartMagnetTimer();
        }

        private void MakeInvisible()
        {
            if (TryGetComponent<Renderer>(out var rendererComponent)) rendererComponent.enabled = false;
            if (TryGetComponent<Collider2D>(out var c)) c.enabled = false;
        }

        private IEnumerator RevertRadiusAfterDelay(CircleCollider2D playerCollider, float delay,
            float originalRadius = DefaultRadius)
        {
            yield return new WaitForSeconds(delay);
            playerCollider.radius = originalRadius;
            Destroy(gameObject);
        }
    }
}