using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    SpriteRenderer sp;

    private float hp = 5;
    public float flickTimer = 0.5f;
    public float flickC = 3;
    public bool flicking = false;
 

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flicking)
        {
            if (flickC > 0)
            {
                flickTimer -= 1 * Time.deltaTime;

                if (flickTimer > 0.35f)
                {
                    sp.enabled = false;
                }
                else if(flickTimer < 0.35f && flickTimer > 0)
                {
                    sp.enabled = true;
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
            sp.enabled = true;
        }
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy" && !flicking)
        {
            if(hp > 0)
            {
                reduceHealth();

                flicking = true;
                flickC = 4;
                flickTimer = 0.5f;
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-3000, 300));
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
