using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleScript : MonoBehaviour
{
    // public Rigidbody2D rigidbody2D;
    public float moveSpeed = 5f;
    public LogicManagerScript logic;
    
    // screen boundaries
    private const float MinX = -3.8f;
    private const float MaxX = 3.8f;
    private const float MinY = -2f;
    private const float MaxY = 2f;
    
    void Start()
    {
        logic = GameObject
            .FindWithTag("Logic")
            .GetComponent<LogicManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // WE can use this for the "flappy bird/jetpack joyride" style
        // If you'd like to try this out you need to make the 0.25 and the gravity 1
        
        // if (Input.GetKeyDown(KeyCode.UpArrow))
        // {
        //     rigidbody2D.velocity = Vector2.up * 5;
        // }
        // float moveStep = moveSpeed * Time.deltaTime;  // Calculate move step once
        // if (Input.GetKey(KeyCode.UpArrow))
        // {
        //     if(transform.position.y < 2)
        //         transform.position += moveStep * Vector3.up;
        // }
        // else if (Input.GetKey(KeyCode.DownArrow))
        // {
        //     if(transform.position.y > -2)
        //         transform.position += moveStep * Vector3.down;
        // }
        MoveWithMouse();
    }
    
    void MoveWithMouse()
    {
        var camera = Camera.main;
        if (!camera)
            return;
        var mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        var clampedX = Mathf.Clamp(mousePosition.x, MinX, MaxX);
        var clampedY = Mathf.Clamp(mousePosition.y, MinY, MaxY);
        var targetPosition = new Vector3(clampedX, clampedY, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
    
} 
