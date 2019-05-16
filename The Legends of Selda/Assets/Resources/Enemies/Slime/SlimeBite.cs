using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBite : MonoBehaviour
{
    Transform transform;
    Animator animator;

    private bool canAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();

        animator = transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("canAttack", canAttack);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Player" ? canAttack = true : canAttack = false) ;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == "Player")
            canAttack = false;
    }
}
