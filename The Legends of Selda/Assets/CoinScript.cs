using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    // Esta clase se encarga de manejar todo lo relacionado
    // Con la moneda, hacer que desaparezca, el sonido...

    public AudioClip audioClip;
    public AudioSource source;

    //Asignamos el sonido a la moneda en cuanto se crea.
    private void Start()
    {
        source.clip = audioClip;
    }
     
    // Este metodo se llama al entrar en colision
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Aqui especificamos que es con el jugador con el que tiene que hacer algo
        if (other.name.Equals("Player"))
        {
            // Llamamaos a el "hilo" aquí
            StartCoroutine(auxMethod());
        }
    }

    // USamos estopara poder ejecutarlo con un timepo de espera
    // queremos el tiempo de espera para que haya el sonido antes
    // de que se destruya el objeto
    private IEnumerator auxMethod()
    {
        //Ejecutamos el sonido
        source.Play();
        // Desactivamos el collider para que no collisione con el otro collider del jugador
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        // Hacemos invisibles la moneda
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        // Hacemos esperar al codigo mientras que el sonido esté activo
        yield return new WaitWhile(() => source.isPlaying);
        // Finalemnte destruimos el objeto
        Destroy(gameObject);
        // Le damos al jugador un punto en el nivel
        GameMasterScript.AddPoint();
    }
}
