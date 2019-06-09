using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private SpriteRenderer sp;
    private Transform transform;
    private Rigidbody2D rb;

    public static float hp = 10;
    public float maxHp = 10;
    public Image[] hearts;
    public Sprite fullHearth;
    public Sprite emptyHearth;

    public PlayerDash playerDash;

    public GameObject gameOverScreen;

    private float flickTimer = 0.5f;
    private float flickC = 3;
    private bool flicking = false;

    public AudioSource source;
    public AudioClip sHurted;
    public AudioClip sDead;


    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        gameOverScreen.SetActive(false);
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
        ManageHealth();
    }

    // --------------------------------------------------------------------
    // Collisions and triggers --------------------------------------------

    private void detectEnemies(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy" && !flicking)
        {
            if (hp > 0)
            {
                source.clip = sHurted;
                source.Play();
                hp -= 1f;
                Debug.Log("Vida restante: " + hp);

                flicking = true;
                flickC = 3;
                flickTimer = 0.5f;

                PlayerMovement.canMove = false;

                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                if (collision.collider.GetComponent<Transform>().localPosition.x > transform.localPosition.x)
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-400, 300));
                else
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(400, 300));
            }

            if (hp == 0)
            {
                gameOver();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        detectEnemies(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        detectEnemies(collision);
    }


    //As the bat is a triggerer we hace to duplicate this
    private void detectEnemiesTrig(Collider2D collider)
    {
        if (collider.tag == "Enemy" && !flicking)
        {
            if (hp > 0)
            {
                source.clip = sHurted;
                source.Play();
                hp -= 1f;

                flicking = true;
                flickC = 3;
                flickTimer = 0.5f;
                
                PlayerMovement.canMove = false;
                rb.velocity = new Vector3(0, 0);

                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

                if (collider.GetComponent<Transform>().localPosition.x > transform.localPosition.x)
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-400, 300));
                else
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(400, 300));
            }

            if (hp==0)
            {
                gameOver();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        detectEnemiesTrig(collider);
    }

    // --------------------------------------------------------------------
    // Extra methods ------------------------------------------------------

    private void ManageHealth()
    {
        //Here we make sure the maxHP is not bigger than the actual hp
        if (maxHp < hp)
            hp = maxHp;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < maxHp)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;


            if (i < hp) {
                hearts[i].sprite = fullHearth;
            }
            else
            {
                hearts[i].sprite = emptyHearth;
            }
        }
    }

    // Metodo que se encarga de hacer la muerte del personaje
    // Además reinicia los puntos del nivel
    private void gameOver()
    {
        // Para que solo se ejecute una vez ponemos un temporizador
        if (flicking)
        {
            // Establecemos el sonido del personaje
            source.clip = sDead;
            // Ejecutamos el sonido
            source.Play();
            // Enseñamos la pantalla de fin del juego
            gameOverScreen.SetActive(true);
            // Antes de destruir el objeto damos tiempo
            // para que termine de reproducir el sonido.
            Destroy(gameObject, 1.7f);
        }
    }
}
