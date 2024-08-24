using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Turtle
{
    public class TurtleScript : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public LogicManagerScript logic;
        private Camera _camera;

        // screen boundaries
        public float MinX, MinY, MaxX, MaxY;

        private bool invincible = false;

        private GameObject TurtleBase;
        private GameObject TurtleBubble;

        private void Start()
        {
            _camera = Camera.main;
            logic = GameObject
                .FindWithTag("Logic")
                .GetComponent<LogicManagerScript>();

            TurtleBase = transform.Find("TurtleBase").gameObject;
            TurtleBubble = transform.Find("TurtleBubble").gameObject;

            // Ensure that the bubble sprite is initially inactive
            TurtleBubble.SetActive(false);
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
            Debug.LogWarning(collision.gameObject.tag);

            if (collision.gameObject.tag == "BubblePowerUp")
            {
                Debug.LogWarning(collision.gameObject.tag);
                StartCoroutine(SwitchToBubbleAndBack()); // this also makes the buddy invincible
                var turtle = GameObject.FindWithTag("Player");
            }
            else if(collision.gameObject.tag == "SharkCollision" && !invincible){
                logic.GameOver();
            }
            

        }
        private IEnumerator SwitchToBubbleAndBack()
        {
            // Switch to turtleBubble
            TurtleBase.SetActive(false);
            TurtleBubble.SetActive(true);
            invincible = true;
            // Wait for 15 seconds
            yield return new WaitForSeconds(15f);

            // Switch back to turtleBase
            TurtleBubble.SetActive(false);
            TurtleBase.SetActive(true);
            invincible = false;
        }
    }
}