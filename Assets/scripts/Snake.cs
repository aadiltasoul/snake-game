using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    private Vector2 direction = Vector2.right; // Snake starts moving to the right
    private List<Transform> snakeBody = new List<Transform>(); // List to store the body parts
    public Transform bodyPrefab; // The prefab for the snake's body
    public float distanceBetweenParts = 0.3f; // Distance between body parts

    void Update()
    {
        // Get input for direction
        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down)
        {
            direction = Vector2.up;
            transform.rotation = Quaternion.Euler(0, 0, 0); // Rotate to face UP
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up)
        {
            direction = Vector2.down;
            transform.rotation = Quaternion.Euler(0, 0, 180); // Rotate to face DOWN
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right)
        {
            direction = Vector2.left;
            transform.rotation = Quaternion.Euler(0, 0, 90); // Rotate to face LEFT
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)
        {
            direction = Vector2.right;
            transform.rotation = Quaternion.Euler(0, 0, -90); // Rotate to face RIGHT
        }
    }

    void FixedUpdate()
    {
        MoveSnake();
    }

    void MoveSnake()
    {
        // Store the previous position of the head
        Vector2 previousPosition = transform.position;

        // Move the body first
        for (int i = snakeBody.Count - 1; i > 0; i--)
        {
            snakeBody[i].position = snakeBody[i - 1].position;
        }

        // Move the first body part to where the head was
        if (snakeBody.Count > 0)
        {
            snakeBody[0].position = previousPosition;
        }

        // Move the head
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    public void GrowSnake()
    {
        // Create a new body part
        Transform newBodyPart = Instantiate(bodyPrefab);

        // If it's the first body part, place it at the head's previous position
        if (snakeBody.Count == 0)
        {
            newBodyPart.position = transform.position - (Vector3)direction * distanceBetweenParts;
        }
        else
        {
            // Otherwise, place it at the last body's position
            newBodyPart.position = snakeBody[snakeBody.Count - 1].position;
        }

        // Add it to the body list
        snakeBody.Add(newBodyPart);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Debug.Log("Food Eaten! Growing Snake...");
            GrowSnake();
        }
    }
}
