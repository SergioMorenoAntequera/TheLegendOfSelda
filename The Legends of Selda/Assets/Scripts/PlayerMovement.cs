using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    private Rigidbody2D rb;

    public void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity += new Vector2(movementSpeed, 0);

            Debug.Log("Velocity: " + rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity += new Vector2(-movementSpeed, 0);
            Debug.Log("Velocity: " + rb.velocity.y);
        }
    }

}
