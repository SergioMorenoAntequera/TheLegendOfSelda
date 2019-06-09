using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    private Transform transform;
    private GameObject player;
    private Rigidbody2D rb;
    private Vector3 centeredPlayerPosition;

    public float speed = 1.5f;
    public bool moving = false;

    public AudioClip sDead;


    // Start is called before the first frame update
    void Start()
    {
        transform = this.GetComponent<Transform>();
        player = GameObject.Find("Player");
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //We use this variable bc the original one is on his feet and i want the bat to go to his chest
        if (player != null)
        {
            centeredPlayerPosition = new Vector3(player.transform.localPosition.x, player.transform.localPosition.y + 1, player.transform.localPosition.z);

            if (moving)
            {
                //To get the direction for the player we hava to minus the (playePositions - batPosition) and normalize it
                transform.Translate((centeredPlayerPosition - transform.localPosition).normalized * Time.deltaTime * speed);
            }


            // ----------------------------------------------------------------------------------------------

            //To rotate the bat so it dosent fly backwards
            if (player.transform.localPosition.x > transform.localPosition.x)  //Player at the right
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else //Player at the left
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
