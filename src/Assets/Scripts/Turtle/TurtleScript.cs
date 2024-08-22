using UnityEngine;

namespace Turtle
{
    public class TurtleScript : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public LogicManagerScript logic;
        private Camera _camera;

        // screen boundaries
        public float MinX, MinY, MaxX, MaxY;


        private void Start()
        {
            _camera = Camera.main;
            logic = GameObject
                .FindWithTag("Logic")
                .GetComponent<LogicManagerScript>();
        }

        private void Update()
        {
            MoveWithMouse();
        }

        private void MoveWithMouse()
        {
            if (!_camera) return;
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var clampedX = Mathf.Clamp(mousePosition.x, MinX, MaxX);
            var clampedY = Mathf.Clamp(mousePosition.y, MinY, MaxY);
            var targetPosition = new Vector3(clampedX, clampedY, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "CoinCollision")
            {
                
                Debug.LogWarning(collision.gameObject.tag);

                logic.GameOver();
            }
            
        }
    }
}