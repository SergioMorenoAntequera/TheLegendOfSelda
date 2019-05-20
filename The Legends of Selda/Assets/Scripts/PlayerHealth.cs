using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private SpriteRenderer sp;
    private Transform transform;

    private float hp = 5;
    public float flickTimer = 0.5f;
    public float flickC = 3;
    public bool flicking = false;
 

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flicking)
        {
            if (flickC > 0)
            {
                flickTimer -= 1 * Time.deltaTime;
                
                if (flickTimer > 0.40f || flickTimer < 0.30f && flickTimer > 0.20f)
                {
                    sp.enabled = true;
                }
                else if(flickTimer < 0.40f && flickTimer > 0.30f || flickTimer < 0.20f && flickTimer > 0.10f)
                {
                    sp.enabled = false;

                    if (flickTimer < 0.20f && flickTimer > 0.10f)
                    {
                        PlayerMovement.canMove = true;
                    }
                }
                else if(flickTimer < 0)
                {
                    flickTimer = 0.5f;
                    flickC--;
                }
            } else
            {
                flicking = false;
            }
        } else
        {
            PlayerMovement.canMove = true;
            sp.enabled = true;
        }
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy" && !flicking)
        {
            if(hp > 0)
            {
                Debug.Log("Player Health.69");
                reduceHealth();

                flicking = true;
                flickC = 3;
                flickTimer = 0.5f;

                PlayerMovement.canMove = false;

                if (collision.collider.GetComponent<Transform>().localPosition.x > transform.localPosition.x)
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-400, 300));
                else
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(400, 300));
            }

            if (hp == 0)
                gameOver();
        }
    }

    private void reduceHealth()
    {
        hp -= 0.5f;
        Debug.Log("Vida restante: " + hp);
    }

    private void gameOver()
    {
        Debug.Log("GAME OVER BBY");
        Destroy(gameObject);
    }
}
