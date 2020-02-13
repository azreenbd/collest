using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UserData : MonoBehaviour
{
    //REMOVE THIS LATER, FRO DEBUGGING ONLY
    public TMP_Text debug;

    // user data is stored here
    public User user = null;

    // is user data assigned?
    bool isAvailable = false;

    // retrive base url from API class
    private string url = API.url;
    // retrieve jwt from login
    string jwt = UserManagement.GetToken();

    // for json response serialization
    ValidateUser response = new ValidateUser();
    private WebToken newToken = new WebToken();

    int numOfError = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        if (!string.IsNullOrEmpty(this.jwt))
        {
            // retrieve new jwt
            RefreshUser();
            // retrieve user data from jwt
            GetUser();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // reassign jwt if there is an update
        this.jwt = UserManagement.GetToken();

        // refresh user data to check for real time change
        /*if (!string.IsNullOrEmpty(this.jwt))
        {
            // retrieve new jwt
            RefreshUser();
            // retrieve user data from jwt
            GetUser();
        }*/

        // if data is assigned
        if (isAvailable)
        {
            user = response.data;

            // show user data
            /*debug.SetText("id: " + user.id
                        + "\nun:" + user.username
                        + "\nemail:" + user.email
                        + "\ndate:" + user.date
                        + "\nxp:" + user.xp
                        + "\ngroup:" + user.group.name);*/
        }
    }
    void RefreshUser()
    {
        StartCoroutine(RefreshToken());
    }

    void GetUser()
    {
        StartCoroutine(DecryptData());
    }

    private IEnumerator RefreshToken()
    {
        while (true) {
            WWWForm form = new WWWForm();

            form.AddField("jwt", this.jwt);

            using (UnityWebRequest www = UnityWebRequest.Post(url + "update-token.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);

                    numOfError++;
                    debug.SetText(numOfError.ToString());
                }
                else
                {
                    newToken = JsonUtility.FromJson<WebToken>(www.downloadHandler.text);

                    UserManagement.SetToken(newToken.jwt);
                }
            }
        }
    }
    private IEnumerator DecryptData()
    {
        while (true)
        {
            WWWForm form = new WWWForm();

            form.AddField("jwt", this.jwt);

            using (UnityWebRequest www = UnityWebRequest.Post(url + "validate-token.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);

                    // the jwt is invalid, so user need to login again to get a new jwt
                    //SceneManager.LoadScene("Login");
                    numOfError++;
                    debug.SetText(numOfError.ToString());
                }
                else
                {
                    // assign all user data
                    response = JsonUtility.FromJson<ValidateUser>(www.downloadHandler.text);

                    isAvailable = true;
                }
            }
        }
    }
}
