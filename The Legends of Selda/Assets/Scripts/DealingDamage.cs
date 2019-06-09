using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class DealingDamage : MonoBehaviour
{
    //This script detects if we hit an enemy
    //and saves his name to let them interact??

    Transform playerTransform;
    public static GameObject enemy;
    public static string enemyName = "";
    private float startAuxTimer = 0.1f;
    private float auxTimer;

    private void Start()
    {
        playerTransform = gameObject.GetComponent<Transform>().parent.gameObject.GetComponent<Transform>();
        auxTimer = startAuxTimer;
    }

    private void Update()
    {
        if (auxTimer > 0) {
            auxTimer -= 1 * Time.deltaTime;
        } else {
            enemyName = "";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Here we can diferenciate between the diferent enemies
        if (other.tag == "Enemy")
        {
            enemyName = other.name;
            enemy = GameObject.Find(enemyName);
            auxTimer = startAuxTimer;
        }
    }
}
