using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    private GameObject player;
    private Transform transform;
    private Rigidbody2D rb;
    private Animator animator;

    public float enemyMovementSpeed = 50f;
    private bool canMove = true;
    private bool facingRight = false;
    private Vector3 localScale;
    private float repeleTimer = 0;
    private int hp;
    private int timesAttacked = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        hp = Random.Range(3, 5);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("moving", canMove);

        /*if (repeleTimer > 0)
        {
            repeleTimer -= 1 * Time.deltaTime;
        }*/
    }

    private void FixedUpdate()
    {
        if (canMove) {
            rb.velocity = new Vector2(-enemyMovementSpeed * Time.deltaTime, rb.velocity.y);
            
            //Rotating the enemy
            if (facingRight)
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    // ******************************************************************************************************
    // As the Sword is a trigger we have to use this method to detect it

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Sword")
        {
            //Here we look for the player
            player = collider.gameObject.GetComponent<Transform>().parent.gameObject;
            hp--;

            if (hp <= 0)
            {
                animator.SetTrigger("die");
                canMove = false;
                rb.velocity = new Vector2(0, 0);
                //Here we also call the destroy Method but in the animator.

            }
            else
            {
                Debug.Log("Vida del slime: " + hp);
                repele(facingRight, rb);
                canMove = false;
                animator.SetBool("hurted", true);
            }
        }
    }

    // ******************************************************************************************************
    // For the rest of the things we'll use this one

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Walls" || collision.collider.tag == "Enemy")
        {
            enemyMovementSpeed *= -1;
            facingRight = !facingRight;
        }

        if(collision.collider.tag == "Player")
        {
            canMove = false;
        }

        animator.SetBool("hurted", false);
        timesAttacked = 0;
        canMove = true;
    }

    // ******************************************************************************************************
    // Extra methods 

    private void repele(bool facingRight, Rigidbody2D rb)
    {
        if (timesAttacked < 1)
        {
            if (player.transform.position.x > transform.position.x)
                rb.AddForce(new Vector2(-200, 200));
            else if(player.transform.position.x < transform.position.x)
                rb.AddForce(new Vector2(200, 200));
            timesAttacked++;
        }
        
    }

    // ******************************************************************************************************
    //Methods used in the animations to avoid writing unnecessary code

    private void destroy()
    {
        Destroy(gameObject);
    }

    
    private void stopMoving()
    {
        canMove = false;
    }

    private void startMoving()
    {
        canMove = true;
    }
    
}
