using System.Collections;
using UnityEngine;

namespace PowerUps.Magnet
{
    public class MagnetPowerUpScript : MonoBehaviour
    {
        public float moveSpeed = 0.001f;
        private const float DefaultRadius = 2;
        public float deadZone = -5;
        private bool _isCollisionActivated = false;
        private GameObject _magnetTimerUi;
        private GameObject _invincibilityTimerUi;
        private bool _isActivated = false;

        public LogicManagerScript logic;

        void Start()
        {
            logic = GameObject
                .FindWithTag("Logic")
                .GetComponent<LogicManagerScript>();
        }

        void Update()
        {
            transform.position += (Vector3.left) * Time.deltaTime;
            if (transform.position.x < deadZone)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("InvincibilityCollision") && !_isActivated)
            {
                _isActivated = true;
                var playerCollider = collision.gameObject.GetComponent<CircleCollider2D>();

                if (playerCollider != null)
                {
                    playerCollider.radius = 0;
                    StartCoroutine(RevertRadiusAfterDelay(playerCollider, 15f));
                    MakeInvisible();
                }

                logic.StartInvincibilityTimer();
            }

            if (collision.gameObject.CompareTag("CoinCollision") && !_isActivated)
            {
                _isActivated = true;
                var playerCollider = collision.gameObject.GetComponent<CircleCollider2D>();

                if (playerCollider != null)
                {
                    playerCollider.radius = 0.9f;
                    StartCoroutine(RevertRadiusAfterDelay(playerCollider, 15f));
                    MakeInvisible();
                }

                logic.StartMagnetTimer();
            }
        }
        
        
        /*private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_isCollisionActivated)
                return;

            var collisionGameObject = collision.gameObject;
            if (collisionGameObject.CompareTag("InvincibilityCollision") &&
                collisionGameObject.CompareTag("CoinCollision"))
            {
                var playerCollider = collisionGameObject.GetComponent<CircleCollider2D>();
                logic.StartInvincibilityTimer();
                
                
                if (playerCollider != null)
                {
                    playerCollider.radius = 0;
                    StartCoroutine(RevertRadiusAfterDelay(playerCollider, 15f));
                    MakeInvisible();
                }
                
            }

           

            if (collisionGameObject.CompareTag("InvincibilityCollision"))
            {
                _isCollisionActivated = true;

                if (playerCollider != null)
                {
                    playerCollider.radius = 0;
                    StartCoroutine(RevertRadiusAfterDelay(playerCollider, 15f));
                    MakeInvisible();
                }

                logic.StartInvincibilityTimer();
            }

            if (collisionGameObject.CompareTag("CoinCollision"))
            {
                _isCollisionActivated = true;

                if (playerCollider != null)
                {
                    playerCollider.radius = 0.9f;
                    StartCoroutine(RevertRadiusAfterDelay(playerCollider, 15f));
                    MakeInvisible();
                }

                logic.StartMagnetTimer();
            }
        }*/

        private void MakeInvisible()
        {
            var renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
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
            yield return new WaitForSeconds(delay);
            playerCollider.radius = originalRadius;
            Destroy(gameObject);
        }
    }
}