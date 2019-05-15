using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;

    private int attackNum = 0;
    private bool attacking = false;

    public float attackTime;
    public float startAttackTime = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            if (attackNum == 1 && attackTime > 0)
            {
                animator.SetTrigger("attack1");

            }
            else if (attackNum == 2 && attackTime > 0)
            {
                animator.SetTrigger("attack2");
            }
            else if (attackNum == 3 && attackTime > 0)
            {
                animator.SetTrigger("attack3");
                attackNum = 0;
            }
            attacking = false;
            attackTime -= 1 * Time.deltaTime;
        }
    }

    public void attack()
    {
        if (!attacking)
        {
            attacking = true;
            attackNum++;
        }
        attackTime = startAttackTime;
    }
}
