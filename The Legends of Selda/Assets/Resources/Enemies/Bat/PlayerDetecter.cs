using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetecter : MonoBehaviour
{
    Transform transform;
    public AudioClip sFound;
    public AudioSource source;
    private bool playerFound = false;

    GameObject bat;
    Transform batTransform;

    private void Start()
    {
        transform = gameObject.transform;
        bat = transform.parent.gameObject;
        batTransform = bat.GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (!playerFound)
            {
                bat.GetComponent<BatMovement>().moving = true;
                source.clip = sFound;
                source.Play();
                playerFound = true;
            }
            
        }
    }
}
