using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    // Script utilizado para el seguimiento de la cámara
    // al personaje en los distintos niveles

    // Conseguimos el componente que contiene tanto
    // la posicion de la cámara como del personaje
    private GameObject player;
    private Transform transform;
    Scene scene;

    // Declaramos variables para más comodidad
    float playerX, playerY, cameraX, cameraY;

    // Update is called once per frame
    void Update()
    {
        // Recogemos los componentes que habiamos declarado antes
        player = GameObject.Find("Player");
        transform = GetComponent<Transform>();
        scene = SceneManager.GetActiveScene();
        
        // Buscamos si existe el personaje (Está vivo)
        if (player != null)
        {
            // Guardamos las variables de antes
            playerX = player.transform.localPosition.x;
            playerY = player.transform.localPosition.y;

            cameraX = transform.localPosition.x;
            cameraY = transform.localPosition.x;

            // Limitamos según el nivel y la posición del personaje  si
            // la cámara lo va a aseguir y hasta donde, no queremos que muestre
            // una pared vacía al llegar a una esquina, queremos que la cámara
            // muestre la parte de detrás del personaje.

            // Limitaciones de la cámara en el nivel 1
            if (scene.name == "Level1")
            {
                if (playerX > 1.80f && playerX < 79f)
                {
                    transform.localPosition = new Vector3(playerX, transform.localPosition.y, transform.localPosition.z);
                }
            }

            // Limitaciones de la cámara en el nivel 2
            if (scene.name == "Level2")
            {
                if (playerX > 0f && playerX < 80f)
                {
                    transform.localPosition = new Vector3(playerX, playerY + 3f, transform.localPosition.z);

                } else if (playerX > 83f)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, playerY + 3f, transform.localPosition.z);
                } 
            }

            // Limitaciones de la cámara en el nivel 3
            if (scene.name == "Level3")
            {
                if (playerX > 1.80f && playerX < 48f)
                {
                    transform.localPosition = new Vector3(playerX, playerY + 3f, transform.localPosition.z);
                } else if (playerX > 48 && playerY < 78 && playerX < 61.5f) {
                    transform.localPosition = new Vector3(playerX, transform.localPosition.y, transform.localPosition.z);
                }
            }
        }
    }
}
