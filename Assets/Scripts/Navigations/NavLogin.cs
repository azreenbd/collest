using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.SceneManagement;

public class NavLogin : MonoBehaviour
{
    #region Global Variables
    // variable to store UI page
    public GameObject pageLogin, pageRegister;

    UserManagement userManager;

    // variable of Input Field game object
    public TMP_InputField loginUsernameInput, loginPasswordInput;
    public TMP_InputField registerUsernameInput, registerEmailInput, registerPasswordInput, registerConfirmPasswordInput;

    // variable of Text(TMP) game object
    public TMP_Text loginErrorText, registerErrorText;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // hide this element
        pageRegister.gameObject.SetActive(false);
        loginErrorText.gameObject.SetActive(false);
        registerErrorText.gameObject.SetActive(false);

        // get UserManagement object
        userManager = GetComponent<UserManagement>();
    }

    #region Navigation in Current Scene
    // navigate to Login page
    public void NavLoginPage()
    {
        pageRegister.gameObject.SetActive(false);
        pageLogin.gameObject.SetActive(true);
    }

    // navigate to Register page
    public void NavRegisterPage()
    {
        pageLogin.gameObject.SetActive(false);
        pageRegister.gameObject.SetActive(true);
    }
    #endregion

    #region Processing Login & Register Input
    // -- checking credentials --
    public void LoginClick()
    {
        userManager.Login(loginUsernameInput.text, loginPasswordInput.text);
    }

    public void RegisterClick()
    {
        // process account creation
        int result = userManager.Register(registerUsernameInput.text, registerEmailInput.text, registerPasswordInput.text, registerConfirmPasswordInput.text);

        /* --Registration Result Code--
        0: Success
        1: Empty input
        2: Username taken
        3: Invalid email
        4: Email exists
        5: Weak password
        6: Confirm password not match
        7: Unknown error
        */

        switch (result)
        {
            // success
            case 0:
                NavLoginPage(); // navigate to login page

                // account creation successful message
                loginErrorText.gameObject.SetActive(true);
                loginErrorText.SetText("Your new account is ready. Proceed to login.");

                Debug.Log("User registration successful!");
                break;

            // empty input error
            case 1:
                registerErrorText.gameObject.SetActive(true);
                registerErrorText.SetText("Please fill in all required details.");
                break;

            // username already exist error
            case 2:
                registerErrorText.gameObject.SetActive(true);
                registerErrorText.SetText("This username is already taken.");
                break;

            // wrong email input error
            case 3:
                registerErrorText.gameObject.SetActive(true);
                registerErrorText.SetText("Please input a valid email address.");
                break;

            // email already exist error
            case 4:
                registerErrorText.gameObject.SetActive(true);
                registerErrorText.SetText("User with your email address already exists.");
                break;

            // weak password error
            case 5:
                registerErrorText.gameObject.SetActive(true);
                registerErrorText.SetText("The password must be at least 8 characters long \nand contains at least 1 letter and 1 number.");
                break;

            // confirm password not match error
            case 6:
                registerErrorText.gameObject.SetActive(true);
                registerErrorText.SetText("Confirmation password does not match.");
                break;

            default:
                registerErrorText.gameObject.SetActive(true);
                registerErrorText.SetText("Something when wrong, please try again.");
                break;
        }
    }
    #endregion
}
