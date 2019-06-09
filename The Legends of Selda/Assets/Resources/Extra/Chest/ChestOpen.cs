using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{

    Animator animator;
    public GameObject coin;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetTrigger("open");
    }
}
