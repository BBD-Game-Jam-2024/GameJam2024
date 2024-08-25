using System.Collections;
using UnityEngine;

namespace Turtle
{
    public class TurtleScript : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public LogicManagerScript logic;
        private Camera _camera;


        // screen boundaries maybe should use vec?
        [SerializeField] public float minX, minY, maxX, maxY;
        private bool _invincible;

        private GameObject _turtleBase;
        private GameObject _turtleBubble;

        private void Start()
        {
            _camera = Camera.main;
            logic = GameObject
                .FindWithTag("Logic")
                .GetComponent<LogicManagerScript>();

            _turtleBase = transform.Find("TurtleBase").gameObject;
            _turtleBubble = transform.Find("TurtleBubble").gameObject;
            // Ensure that the bubble sprite is initially inactive
            _turtleBubble.SetActive(false);
        }

        private void Update()
        {
            MoveWithMouse();
        }

        private void MoveWithMouse()
        {
            if (!_camera) return;
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var clampedX = Mathf.Clamp(mousePosition.x, minX, maxX);
            var clampedY = Mathf.Clamp(mousePosition.y, minY, maxY);
            var targetPosition = new Vector3(clampedX, clampedY, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("CoinCollision")) return;
            if (collision.gameObject.CompareTag("BubblePowerUp")) StartCoroutine(SwitchToBubbleAndBack());
            else if (collision.gameObject.CompareTag("SharkCollision") && !_invincible)
            {
                gameObject.SetActive(false);
                logic.GameOver();
            }
        }

        private IEnumerator SwitchToBubbleAndBack()
        {
            // Switch to turtleBubble
            _turtleBase.SetActive(false);
            _turtleBubble.SetActive(true);
            _invincible = true;
            // Wait for 15 seconds
            yield return new WaitForSeconds(10f);

            // Switch back to turtleBase
            _turtleBubble.SetActive(false);
            _turtleBase.SetActive(true);
            _invincible = false;
        }
    }
}