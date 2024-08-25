using System.Collections;
using UnityEngine;

namespace PowerUps.Multiply
{
    public class MultiplyPowerUpScript : MonoBehaviour
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
            MakeInvisible();
        }
        
        private void MakeInvisible()
        {
            if (TryGetComponent<Renderer>(out var rendererComponent)) rendererComponent.enabled = false;
            if (TryGetComponent<Collider2D>(out var c)) c.enabled = false;
        }
    }
}