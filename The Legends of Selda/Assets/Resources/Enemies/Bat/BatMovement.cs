using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    Transform transform;
    GameObject player;
    public Vector3 centeredPlayerPosition;

    // Start is called before the first frame update
    void Start()
    {
        transform = this.GetComponent<Transform>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //We use this variable bc the original one is on his feet and i want the bat to go to his chest
        centeredPlayerPosition = new Vector3(player.transform.localPosition.x, player.transform.localPosition.y + 1, player.transform.localPosition.z);
        
        //To get the direction for the player we hava to minus the (playePositions - batPosition) and normalize it
        transform.Translate((centeredPlayerPosition - transform.localPosition).normalized * Time.deltaTime * 1.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.collider.tag == player.tag)
        {
            player.GetComponent<PlayerHealth>();
        }*/
    }
}
