using UnityEngine;

namespace Coin
{
    public class CoinCollisionScript : MonoBehaviour
    {
        public LogicManagerScript logic;

        private void Start()
        {
            logic = GameObject
                .FindWithTag("Logic")
                .GetComponent<LogicManagerScript>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("CoinCollision")) return;
            Destroy(transform.parent.gameObject);
            logic.AddScore(1);
        }
    }
}