using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    string jwt = UserManagement.GetToken();
    public UserData userData;

    public GameObject groupSidebar, inventorySidebar, questSidebar;
    public GameObject messageBox;
    public GameObject panelQuest;
    public TMP_Text textPlayerName;

    // Start is called before the first frame update
    void Start()
    {
        // hide hidden GUI element when scene is launch!!
        groupSidebar.gameObject.SetActive(false);

        messageBox.gameObject.SetActive(false);
        panelQuest.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // Checking for jwt assignment
        // if jwt is not assigned, redirect to login scene
        this.jwt = UserManagement.GetToken();
        if (string.IsNullOrEmpty(this.jwt))
        {
            SceneManager.LoadScene("Login");
        }

        textPlayerName.SetText(userData.user.username);

    }
}
