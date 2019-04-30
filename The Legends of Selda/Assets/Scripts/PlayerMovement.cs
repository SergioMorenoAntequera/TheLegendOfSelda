using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement and Jumping")]
    public float movementSpeed = 400f;
    private float movingDirection = 0f;
    public float jumpingSpeed = 8f;
    private bool jumping = false;
    private bool facingRight = true;
    private float fallMultiplier = 3.5f;
    private float lowJumpMultiplier = 3f;
    float xScale;

    //Collisions
    List<Collider2D> groundTouched = new List<Collider2D>();

    private Rigidbody2D rb;
    private Transform transform;
    private Animator animator;

    public Joystick joystick;

    public void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        transform = gameObject.GetComponent<Transform>();
        animator = gameObject.GetComponent<Animator>();

        xScale = transform.localScale.x;
    }

    public void Update()
    {
        //********************************
        // ***** Horizontal Movement *****
        if (joystick.Horizontal <= -0.2f) //LEFT
        {

            movingDirection = -1f;
            facingRight = false;
            animator.SetInteger("moving", 1);
        }

        if (joystick.Horizontal >= 0.2f) //RIGHT
        {
            movingDirection = 1f;
            facingRight = true;
            animator.SetInteger("moving", 1);
        }

        if (joystick.Horizontal == 0)
        {
            movingDirection = 0f;
            animator.SetInteger("moving", 0);
        }
        
        //*****************************
        // ***** Jumping Movement *****
        if (joystick.Vertical >= 0.5f && groundTouched.Count != 0)
        {
            rb.AddForce(new Vector2(0, jumpingSpeed));
            jumping = true;
        }

        
       
    }

    public void FixedUpdate()
    {
        // ***** Horizontal Movement *****
        rb.velocity = new Vector2(movingDirection * movementSpeed * Time.deltaTime, rb.velocity.y);

        // ***** Horizontal Rotation *****
        if (facingRight)
            xScale = Mathf.Abs(xScale);
        else
            xScale = Mathf.Abs(xScale) * -1;

        transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);

        // ***** Jumping *****
        if (jumping)
        {
            //rb.velocity += Vector2.up * jumpingSpeed;
            rb.AddForce(Vector2.up * jumpingSpeed, ForceMode2D.Impulse);
            jumping = false;
        }
        
        // This controls how the gravity affects our player to give a better jumping experience
        if(rb.velocity.y < 0)
            rb.gravityScale = fallMultiplier;
        else 
            if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
                rb.gravityScale = lowJumpMultiplier;
            else
                rb.gravityScale = 2f;
    }

    //Adds to a list all th colliders that are actually touching our character
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //We create an array to store all the collisions on our colliders
        ContactPoint2D[] points = new ContactPoint2D[2]; //2 as an example
        collision.GetContacts(points);
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i].normal == Vector2.up && !groundTouched.Contains(collision.collider))
            {
                groundTouched.Add(collision.collider);
                return;
            }
        }
    }

    //Removes the colliders that are no longer in contact with our character
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (groundTouched.Contains(collision.collider))
        {
            groundTouched.Remove(collision.collider);
        }
    }
}
