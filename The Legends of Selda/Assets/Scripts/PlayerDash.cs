using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

    [Header("Dashing properties")]
    private float dashSpeed = 700f;
    private float dashTime = 0f;
    private float startDashTime = .3f;

    private bool canDash = true;
    public bool dashing = false;
    

    // Start is called before the first frame update
    void Start()
    {
        startDashTime = .3f;
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (dashing)
        {
            animator.SetTrigger("skill");
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

            dashTime -= 1 * Time.deltaTime;

            if (PlayerMovement.facingRight)
            {
                rb.AddForce(new Vector2(dashSpeed, 0));
            } else {
                rb.AddForce(new Vector2(-dashSpeed, 0));
            }
            

            if (dashTime <= 0f)
            {
                dashing = false;
                animator.ResetTrigger("skill");
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.velocity = new Vector2(0, 0);
            }
        }

        if (!canDash && PlayerMovement.groundTouched.Count != 0)
        {
            canDash = true;
        }
    }

    public void Dash() {

        if (canDash)
        {
            dashing = true;
            dashTime = startDashTime;
            canDash = false;
        }
    }
}
