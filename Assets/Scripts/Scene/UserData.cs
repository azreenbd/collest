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

    // is data assigned?
    bool isAvailable = false;

    ValidateUser user = new ValidateUser();
    private WebToken newToken = new WebToken();
    string jwt = UserManagement.GetToken();

    // Start is called before the first frame update
    void Start()
    {
        if (!string.IsNullOrEmpty(this.jwt))
        {
            GetUser();

            // show user data
            /*debug.SetText("id: " + user.data.id
                        + "\nun:" + user.data.username
                        + "\nemail:" + user.data.email
                        + "\ndate:" + user.data.date
                        + "\nxp:" + user.data.xp
                        + "\ngroup:" + user.data.group);*/
        }
            

        // DO hide hidden GUI element when scene is launch!!
    }

    // Update is called once per frame
    void Update()
    {
        this.jwt = UserManagement.GetToken();

        if (!string.IsNullOrEmpty(this.jwt))
        {
            RefreshUser();
            GetUser();

            Debug.Log(this.jwt);
        }

        if (isAvailable)
        {
            // show user data
            debug.SetText("id: " + user.data.id
                        + "\nun:" + user.data.username
                        + "\nemail:" + user.data.email
                        + "\ndate:" + user.data.date
                        + "\nxp:" + user.data.xp
                        + "\ngroup:" + user.data.group);
        }
    }
    void RefreshUser()
    {
        StartCoroutine(RefreshToken(this.jwt));
    }

    void GetUser()
    {
        StartCoroutine(DecryptData(this.jwt));
    }

    private IEnumerator RefreshToken(string jwt)
    {
        WWWForm form = new WWWForm();

        form.AddField("jwt", jwt);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/testapi/api/update-token.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                newToken = JsonUtility.FromJson<WebToken>(www.downloadHandler.text);

                UserManagement.SetToken(newToken.jwt);
            }
        }
    }
    private IEnumerator DecryptData(string token)
    {
        WWWForm form = new WWWForm();

        form.AddField("jwt", token);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/testapi/api/validate-token.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);

                // the jwt is invalid, so user need to login again to get a new jwt
                SceneManager.LoadScene("Login");
            }
            else
            {
                // assign all user data
                user = JsonUtility.FromJson<ValidateUser>(www.downloadHandler.text);

                isAvailable = true;
            }
        }
    }
}
