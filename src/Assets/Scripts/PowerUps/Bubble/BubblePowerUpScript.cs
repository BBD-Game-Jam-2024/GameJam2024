using System.Collections;
using UnityEngine;

namespace PowerUps.Bubble
{
    public class BubblePowerUpScript : MonoBehaviour
    {
        private GameObject _bubbleTimerUi;
        private const float DefaultRadius = 2;
        private bool _isActivated;
        public LogicManagerScript logic;

        // Start is called before the first frame update
        private void Start()
        {
            logic = GameObject
                .FindWithTag("Logic")
                .GetComponent<LogicManagerScript>();
        }

        // Update is called once per frame
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("CoinCollision") || _isActivated) return;

            _isActivated = true;
            var playerCollider = collision.gameObject.GetComponent<CircleCollider2D>();

            if (playerCollider != null)
            {
                playerCollider.radius = 0;
                StartCoroutine(RevertRadiusAfterDelay(playerCollider, 10f));
                MakeInvisible();
            }

            logic.StartInvincibilityTimer();
        }

        private void MakeInvisible()
        {
            var rendererComponent = GetComponent<Renderer>();
            if (rendererComponent != null) rendererComponent.enabled = false;

            var colliderComponent = GetComponent<Collider2D>();
            if (colliderComponent != null) colliderComponent.enabled = false;
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