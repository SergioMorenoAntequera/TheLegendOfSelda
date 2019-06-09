using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Este script se ocupa de manejar la informacion para al terminar
    // aparezcan las puntuaciones correctas.

    // Un objeto de la clase texto de la que sacaremos un string
    Text text;

    // Conseguimos el componene necesario del objeto al que le vamos
    // a aplicar los cambios.
    private void Awake()
    {
        text = gameObject.GetComponent<Text>();
    }

    // Haremos que se esté actualizando todo el rato ya que normalmente estará
    // desacativado y no significará ninguna carga en el programa.
    private void Update()
    {

        Debug.Log("1: " + GameMasterScript.scoreLvl1);
        Debug.Log("2: " + GameMasterScript.scoreLvl2);
        Debug.Log("3: " + GameMasterScript.scoreLvl3);

        // Cuando se active saltará este código que se encarga de:
        // 1.- Comparar las puntuaciones actuales con las guardadas.
        // 2.- Sustituir estas puntuaciones si son mejores.
        int totalScoreSaved = FirebaseData.getScoreLvl1() + FirebaseData.getScoreLvl2() + FirebaseData.getScoreLvl3();
        int totalScoreActual = GameMasterScript.scoreLvl1 + GameMasterScript.scoreLvl2 + GameMasterScript.scoreLvl3;

        // Aquí mostramos el mensaje para saber si hemos avanzado
        // en la puntuación total.
        if (totalScoreActual > totalScoreSaved) {
            text.text = "¡NUEVO RECORD!\n" +
                "Score: " + totalScoreActual;
            
        }

        // Como queremos conservar la mejor puntuacion dentro de cada nivel
        // comprobamos la puntuacion en cada uno y si vemos que es mejor
        // la cambiamos
        if (GameMasterScript.scoreLvl1 > FirebaseData.getScoreLvl1())
            FirebaseData.setScoreLvl1(GameMasterScript.scoreLvl1);
        else if (GameMasterScript.scoreLvl2 > FirebaseData.getScoreLvl2())
            FirebaseData.setScoreLvl2(GameMasterScript.scoreLvl2);
        else if (GameMasterScript.scoreLvl3 > FirebaseData.getScoreLvl3())
            FirebaseData.setScoreLvl3(GameMasterScript.scoreLvl3);

        // Si la puntuacion no es mayor simplemente enseñamos un mensaje.
        if (totalScoreActual < totalScoreSaved)
            text.text = "Score: " + totalScoreActual + "\nMejor puntuación: " + totalScoreSaved;
    }
}
