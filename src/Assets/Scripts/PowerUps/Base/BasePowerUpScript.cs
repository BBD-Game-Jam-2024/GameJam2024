using UnityEngine;

namespace PowerUps.Base
{
    public class BasePowerUpScript : MonoBehaviour
    {
        [SerializeField] private GameObject uiIndicator;
        private bool _isActive;
        private LogicManagerScript _gameLogic;

        private void Start()
        {
            _gameLogic = GameObject
                .FindWithTag("Logic")
                .GetComponent<LogicManagerScript>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Don't want to re-enable if already active.
            if (_isActive) return;

            _isActive = true;
            var playerCollider = collision.gameObject.GetComponent<CircleCollider2D>();
            // Return if no active collision for player.
            if (!playerCollider) return;


            playerCollider.radius = 0.065f;
            // StartCoroutine(RevertRadiusAfterDelay(playerCollider, 15f));
            // MakeInvisible();
        }
    }
}