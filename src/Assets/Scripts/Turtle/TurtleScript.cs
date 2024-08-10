using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleScript : MonoBehaviour
{
    // public Rigidbody2D rigidbody2D;
    public float moveSpeed = 5f;
    public LogicManagerScript logic;
    
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
        float moveStep = moveSpeed * Time.deltaTime;  // Calculate move step once
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if(transform.position.y < 2)
                transform.position += moveStep * Vector3.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if(transform.position.y > -2)
                transform.position += moveStep * Vector3.down;
        }
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     logic.GameOver();
    // }
} 
