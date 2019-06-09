using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseData : MonoBehaviour
{
    public InputField ifUser, ifPass;
    public static string usernameToEdit = "Nombre de usuario";
    public static string passwordToEdit = "Contraseña";

    public Text tWarning;
    private bool firebaseReady = false;
    static DatabaseReference root = null;
    private bool encontrado = false;
    private string pass = "";

    static DataSnapshot currentUser = null;
    int scoreTest = 0;

    private void Start()
    {
       FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://laleyendadeselda-8206f.firebaseio.com/");

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                // app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                firebaseReady = true;
                root = FirebaseDatabase.DefaultInstance.RootReference;
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                Debug.Log("Error trying to access firebase");
                firebaseReady = false;
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    // *********************************************************************************************
    // Main funtions methods ***********************************************************************

    public void logIn()
    {
        usernameToEdit = ifUser.text;
        passwordToEdit = ifPass.text;
        login(ifUser.text);
    }

    public void registerAsync()
    {
        usernameToEdit = ifUser.text;
        passwordToEdit = ifPass.text;
        register(ifPass.text);
    }

    // *********************************************************************************************
    // Extra methods *******************************************************************************

    //REGISTER *************************************************************************************
    private async System.Threading.Tasks.Task register(string userName)
    {
        encontrado = false;

        // Here we are looking for the names(code from Firebase documentation)
        await FirebaseDatabase.DefaultInstance
        .GetReference("User")
        .GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.Log("UIController.77: An error has occurred");
                Debug.Log("UIController.78: no existe");

            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;                 // Here is the User element
                IEnumerable<DataSnapshot> users = snapshot.Children; // Here we have the users

                // Do something with snapshot...
                foreach (var user in users)
                {
                    //Getting the passwords
                    if (user.Key.Equals(userName))
                    {
                        encontrado = true;
                    }
                }
            }
        });

        if (!encontrado)
        {
            root.Child("User/" + usernameToEdit + "/Level").SetValueAsync(1);
            root.Child("User/" + usernameToEdit + "/Pass").SetValueAsync(getChechsum(passwordToEdit));
            root.Child("User/" + usernameToEdit + "/BestScoreLvl1").SetValueAsync(0);
            root.Child("User/" + usernameToEdit + "/BestScoreLvl2").SetValueAsync(0);
            root.Child("User/" + usernameToEdit + "/BestScoreLvl3").SetValueAsync(0);
            Debug.Log("Usuario registrado con exito antes");
            tWarning.text = "Usuario registrado con exito";
            Debug.Log("Usuario registrado con exito despues");
        }
        else
        {
            tWarning.text = "Este nombre de usuario ya está ocupado";
        }
    }

    //LOG IN *************************************************************************************
    private async System.Threading.Tasks.Task login(string userName)
    {
        encontrado = false;
        
        // Here we are looking for the names(code from Firebase documentation)
        await FirebaseDatabase.DefaultInstance
        .GetReference("User")
        .GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.Log("UIController.77: An error has occurred");
                Debug.Log("UIController.78: no existe");
                    
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;                 // Here is the User element
                IEnumerable<DataSnapshot> users = snapshot.Children; // Here we have the users

                // Do something with snapshot...
                foreach (var user in users)
                {
                    //Getting the passwords
                    if (user.Key.Equals(userName))
                    {
                        encontrado = true;
                        GameMasterScript.currentUser = user;
                        //currentUser = user;
                        pass = user.Child("Pass").Value.ToString();
                        UIController.level = int.Parse(user.Child("Level").Value.ToString());
                        
                    }
                }
            }
        });

        if (encontrado)
        {
            if (pass.Equals(getChechsum(ifPass.text)))
            {
                SceneSwapper.goToSaved();
            } else
            {
                tWarning.text = "Contraseña incorrecta";
            }
        } else
        {
            tWarning.text = "Usuario no encontrado";
        }
        
    }

    //UPDATE THE LEVEL
    public static void updateLevel(int num)
    {
        root.Child("User/" + usernameToEdit + "/Level").SetValueAsync(UIController.level);
    }

    // **********************************************************************************************

    private string getChechsum(string aString)
    {
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            aString = BitConverter.ToString(
              md5.ComputeHash(Encoding.UTF8.GetBytes(passwordToEdit))
            ).Replace("-", String.Empty);
        }
        return aString;
    }


    //Change the actual score of each level with the current one
    public static void setScoreLvl1(int scoreLvl1)
    {
        root.Child("User/" + usernameToEdit + "/BestScoreLvl1").SetValueAsync(scoreLvl1);
    }
    public static void setScoreLvl2(int scoreLvl2)
    {
        root.Child("User/" + usernameToEdit + "/BestScoreLvl2").SetValueAsync(scoreLvl2);
    }
    public static void setScoreLvl3(int scoreLvl3)
    {
        root.Child("User/" + usernameToEdit + "/BestScoreLvl3").SetValueAsync(scoreLvl3);
    }

    //Change the actual score of each level with the current one
    public static int getScoreLvl1()
    {
        return int.Parse(GameMasterScript.currentUser.Child("BestScoreLvl1").Value.ToString());
    }
    public static int getScoreLvl2()
    {
        return int.Parse(GameMasterScript.currentUser.Child("BestScoreLvl2").Value.ToString());
    }
    public static int getScoreLvl3()
    {
        return int.Parse(GameMasterScript.currentUser.Child("BestScoreLvl3").Value.ToString());
    }

    ////////////////////////////////////////////////
    
    public static void setNewScore()
    {
        root.Child("User/" + usernameToEdit + "/BestScore").SetValueAsync(PlayerMovement.score);
    }
    public static int getSavedScore()
    {
        return int.Parse(currentUser.Child("BestScore").Value.ToString());
    }
}
