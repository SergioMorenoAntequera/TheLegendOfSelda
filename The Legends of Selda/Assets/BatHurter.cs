using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatHurter : MonoBehaviour
{
    GameObject bat;
    

    private void Start()
    {
        bat = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Sword")
        {

            bat.GetComponent<BatMovement>().moving = false;
            bat.transform.GetChild(0).GetComponent<AudioSource>().clip = bat.GetComponent<BatMovement>().sDead;
            bat.transform.GetChild(0).GetComponent<AudioSource>().Play();
            GameMasterScript.AddPoint();
            Destroy(bat, 0.2f);
        }
    }
}
