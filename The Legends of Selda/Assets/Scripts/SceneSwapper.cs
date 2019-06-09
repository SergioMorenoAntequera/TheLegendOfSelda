using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    // Este script se encarga de realizar todos los cambios de escenas de todo el juego
    // dependiendo de en que escena te encuentres ya que hay un objeto con esete escript
    // en todas las escenas y al colisionar con él pasa a la siguiente.
  
    public GameObject gameOverScreen;
    
    // Al entrar una colision
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Comprobamos que sea la del jugador
        if (collider.name == "Player")
        {
            //Comprobamos la escena en la que nos encotramos para ir a la siguiente
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                // Infromamos a nuestro controllador de escena de que cambie
                UIController.level = 2;

                // Aqui obtenemos la puntuacion en el nivel 1
                // y si es mejor que la que tenemos la actualizamos
                if (FirebaseData.getScoreLvl1() < GameMasterScript.scoreLvl1)
                    FirebaseData.setScoreLvl1(GameMasterScript.scoreLvl1);
            }


            if (SceneManager.GetActiveScene().name == "Level2")
            {
                // Infromamos a nuestro controllador de escena de que cambie
                UIController.level = 3;

                // Aqui obtenemos la puntuacion en el nivel 1
                // y si es mejor que la que tenemos la actualizamos
                if (FirebaseData.getScoreLvl2() < GameMasterScript.scoreLvl2)
                    FirebaseData.setScoreLvl2(GameMasterScript.scoreLvl2);
            }


            if (SceneManager.GetActiveScene().name == "Level3")
            {
                // Infromamos a nuestro controllador de escena de que ya hemos acabado
                UIController.level = 0;

                // Aqui obtenemos la puntuacion en el nivel 1
                // y si es mejor que la que tenemos la actualizamos
                if (FirebaseData.getScoreLvl3() < GameMasterScript.scoreLvl3)
                    FirebaseData.setScoreLvl3(GameMasterScript.scoreLvl3);

                gameOverScreen.active = true;
                 

                return;
            }

            //Cargamos la escena siguiente
            SceneManager.LoadScene(UIController.level);
            //Actualizamos la base de datos
            FirebaseData.updateLevel(UIController.level);
        }
    }
        
    public void goToMainScreen()
    {
        // Hacemos que todo vuelva al menú principal
        // restablecemos los parametros para dejarlo listo para otra partida
        // Quitamos la pantalla de GameOver
        gameOverScreen.SetActive(false);
        // Le quitamos los puntos al jugador
        GameMasterScript.scoreLvl1 = 0;
        GameMasterScript.scoreLvl2 = 0;
        GameMasterScript.scoreLvl3 = 0;
        // Cargamos el menú principal
        SceneManager.LoadScene(0);
        //Le recuperamos la vida al jugador
        PlayerHealth.hp = 10;
    }

    //Metodo usado para cargar la escena que encuentre en la base de datos
    public static void goToSaved()
    {
        SceneManager.LoadScene(UIController.level);
    }

    // Necesitamos este método para poder llamarlo al pulsaor el botón
    // Al igual que el otro para llamarlo desde código
    public void goToSavedNonStatic()
    {
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(UIController.level);
        restartLevelPoints();
        PlayerHealth.hp = 10;
    }

    // Metodo auxiliar para que al morir se reinicien los puntos del nivel que estamos.
    private void restartLevelPoints()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
            GameMasterScript.scoreLvl1 = 0;
        if (SceneManager.GetActiveScene().name == "Level2")
            GameMasterScript.scoreLvl2 = 0;
        if (SceneManager.GetActiveScene().name == "Level3")
            GameMasterScript.scoreLvl3 = 0;
    }
}
