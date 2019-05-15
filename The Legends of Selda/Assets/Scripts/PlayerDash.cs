using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

    [Header("Dashing properties")]
    public float dashSpeed = 3f;
    public float dashTime = 0f;
    public float startDashTime = 3f;
    private bool dashing = false;
    private bool canDash = true;

    // Start is called before the first frame update
    void Start()
    {
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
