using UnityEngine;

namespace BaseScripts
{
    public class MovementScript : MonoBehaviour
    {
        public float deadZone = -5;
        [SerializeField] private float speed = 1f;

        private void Update()
        {
            transform.position += Vector3.left * (Time.deltaTime * speed);
            if (transform.position.x < deadZone) Destroy(gameObject);
        }
    }
}