using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameMasterScript : MonoBehaviour
{
    // Clase con distintas variables y métodos útiles

    public static DataSnapshot currentUser = null;
    public static int scoreLvl1 = 0, scoreLvl2 = 0, scoreLvl3 = 0;
    private static GameMasterScript gameMaster;

    // Esto lo hacemos para que cuando se inicie el juego
    // Declarar que el objeto que tiene esta clase no se
    // Destruya ya que lo usaremos más adelante
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // Hacemos esto para eliminar los objetos de este tipo
        // que se crean a volver a la escena. Ya que entra en el Start
        // al empezar la escena.
        if (gameMaster == null)
            gameMaster = this;
        else
            DestroyObject(gameObject);
        
    }

    // Llamamos a este punto estemos en el nivel en el que
    // estemos para añadir puntos a los distintos niveles.
    public static void AddPoint()
    {
        // Detecta en que escena estamos comparando el nombre
        // de la escena con las siguientes cadenas y así
        // añade a un contador o a otro.
        if (SceneManager.GetActiveScene().name  == "Level1")
            scoreLvl1++;
        if (SceneManager.GetActiveScene().name == "Level2")
            scoreLvl2++;
        if (SceneManager.GetActiveScene().name == "Level3")
            scoreLvl3++;
    }
}
