using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class DealingDamage : MonoBehaviour
{

    public void Start()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("He golpeado: " + collision.collider.name);
    }
}
