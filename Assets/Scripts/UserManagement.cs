using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using UnityEngine;
using System.Text;
using TMPro;
using UnityEngine.SceneManagement;

public class UserManagement : MonoBehaviour
{
    #region Variables Declaration
    private WebToken jwt = new WebToken();

    // logged in user jwt
    private static string token;

    // variable of Text(TMP) game object
    public TMP_Text loginErrorText, registerErrorText;
    #endregion

    private bool IsValidEmail(string email)
    {
        try
        {
            MailAddress mailAddress = new MailAddress(email);

            return true;
        }
        catch
        {
            return false;
        }
    }

    // check for weak password
    private bool IsWeakPassword(string password)
    {
        Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");

        // return true if doesn't match regex
        return !regex.IsMatch(password);
    }

    // get logged in user jwt
    public static string GetToken()
    {
        return token;
    }
    public static void SetToken(string jwt)
    {
        token = jwt;
    }
    public bool Login(string username, string password)
    {
        Debug.Log(username + "\n" + password);

        // check empty input
        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            // do some rest api stuff here
            StartCoroutine(UserAuth(username, password));

            //if success
            if(jwt.message == "Success")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private IEnumerator UserAuth(string email, string password)
    {
        WWWForm form = new WWWForm();

        //form.AddField("username", username);
        form.AddField("email", email);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/testapi/api/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);

                // show error message
                loginErrorText.gameObject.SetActive(true);
                loginErrorText.SetText("Incorrect username or password.");
            }
            else
            {
                jwt = JsonUtility.FromJson<WebToken>(www.downloadHandler.text);

                // deactivate error message if it is active
                loginErrorText.gameObject.SetActive(false);

                Debug.Log("Mesej: " + jwt.message + "\n" + "token: " + jwt.jwt);

                token = jwt.jwt;

                SceneManager.LoadScene("Lobby");
            }
        }
    }

    public int Register(string username, string email, string password, string confirmPassword)
    {
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

        Debug.Log(username + "\n" + email + " " + password + " " + confirmPassword);

        // check empty input
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            return 1;
        }
        else
        {
            //=========!!!!PUT USERNAME TAKEN ERROR CHECKING HERE!!!!!========================
            if (username == "loremipsum") //retrieve using rest api
            {
                return 2;
            }
            else if (!IsValidEmail(email))
            {
                return 3;
            }
            //========!!!!!PUT EMAIL EXIST ERROR CHECKING HERE!!!!!===========================
            else if (email == "lorem@ipsum.com") //retrieve using rest api
            {
                return 4;
            }
            else if (IsWeakPassword(password))
            {
                return 5;
            }
            else if (password != confirmPassword)
            {
                return 6;
            }
            else
            {
                // do some rest api here to register
                // if response success, return 0, else return 7
                StartCoroutine(CreateUser(username, email, password));


                return 0;

                //TO DO: FIGURE OUT HOW TO GET ERROR WHEN CREATEUSER
            }
        }
    }

    private IEnumerator CreateUser(string username, string email, string password)
    {
        WWWForm form = new WWWForm();

        form.AddField("username", username);
        form.AddField("email", email);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/testapi/api/create-user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}
