using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    string jwt = UserManagement.GetToken();

    // Start is called before the first frame update
    void Start()
    {
        // Checking for jwt assignment
        // if jwt is not assigned, redirect to login scene
        if (string.IsNullOrEmpty(this.jwt))
        {
            SceneManager.LoadScene("Login");
        }



        // DO hide hidden GUI element when scene is launch!!
    }

    // Update is called once per frame
    void Update()
    {

    }
}
