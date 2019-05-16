using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public GameObject player;
    Transform transform;
    Rigidbody2D rb;
    Animator animator;

    private float enemyMovementSpeed = 50f;
    private bool isReceivingDamage = false;
    public bool facingRight = false;
    private Vector3 localScale;
    private float repeleTimer = 0;
    private int hp = 3;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("Velocity", (int)enemyMovementSpeed);

        if (repeleTimer > 0)
        {
            repeleTimer -= 1 * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (!isReceivingDamage) {
            rb.velocity = new Vector2(-enemyMovementSpeed * Time.deltaTime, rb.velocity.y);
           
            if (facingRight)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            } else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    // ******************************************************************************************************
    // As the Sword is a trigger we have to use this method to detect it

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Sword")
        {
            hp--;
            if (hp == 0)
                Destroy(gameObject);
        }
        /*
        if (DealingDamage.enemyName == gameObject.name)
        {
            isReceivingDamage = true;
            if (--hp == 0)
            {
                Destroy(gameObject);
            }

            Debug.Log("VIDA: " + hp);
            rb.velocity = new Vector2(0, 0);

            if(repeleTimer <= 0)
            {
                repele(facingRight, rb);
                repeleTimer = 0.2f;
            }
        }*/
    }

    // ******************************************************************************************************
    // For the rest of the things we'll use this one

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isReceivingDamage && collision.collider.transform.parent.gameObject.name == "Collider")
            isReceivingDamage = false;

        if (collision.collider.name == "Walls")
        {
            enemyMovementSpeed *= -1;
            facingRight = !facingRight;
        }
    }

    private void repele(bool facingRight, Rigidbody2D rb)
    {
        if (player.transform.position.x > transform.position.x)
            rb.AddForce(new Vector2(-200, 200));
        else if(player.transform.position.x < transform.position.x)
            rb.AddForce(new Vector2(200, 200));
    }
}
