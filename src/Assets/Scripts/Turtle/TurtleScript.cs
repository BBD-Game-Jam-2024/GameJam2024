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
        private Coroutine bubbleCoroutine;

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
            Debug.LogWarning("Turtle script:");
            if(collision.gameObject.tag == "CoinCollision"){
                Debug.LogWarning("Turtle colliding with coin");
            }

            if (collision.gameObject.CompareTag("BubblePowerUp"))
            {
                Debug.LogWarning("Turtle colliding with bubblePowerUp");
                if (bubbleCoroutine != null)
                {
                    StopCoroutine(bubbleCoroutine);
                }
                bubbleCoroutine = StartCoroutine(SwitchToBubbleAndBack());
                Debug.LogWarning("Starting invincible timer from turtle");
                logic.StartInvincibilityTimer();
                // invincible = true; // this is changed back to false at end of below coroutine
                // StartCoroutine(SwitchToBubbleAndBack()); // this also makes the buddy invincible
                // invincible = false;
                // var turtle = GameObject.FindWithTag("Player");
            }
            else if (collision.gameObject.CompareTag("SharkCollision") && !_invincible)
            {
                logic.GameOver();
            }
        }

        private IEnumerator SwitchToBubbleAndBack()
        {
            // Switch to turtleBubble
            _turtleBase.SetActive(false);
            _turtleBubble.SetActive(true);
            _invincible = true;
            Debug.LogWarning("now invincible");
            // Wait for 15 seconds
            yield return new WaitForSeconds(10f);
            Debug.LogWarning("Switch back to normal");

            // Switch back to turtleBase
            _turtleBubble.SetActive(false);
            _turtleBase.SetActive(true);
            _invincible = false;
            Debug.LogWarning("Not invincible anymore");
            bubbleCoroutine = null;
        }
    }
}