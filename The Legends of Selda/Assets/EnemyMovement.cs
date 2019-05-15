using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float enemyMovementSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-enemyMovementSpeed * Time.deltaTime, rb.velocity.y);
    }
}
