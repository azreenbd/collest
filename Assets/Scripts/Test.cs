using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Test : MonoBehaviour
{
    public TMP_Text debug;
    ValidateUser player = new ValidateUser();
    //User userData = new User();

    // Start is called before the first frame update
    void Start()
    {
        debug.SetText(UserManagement.GetToken());
        UpdateUser();
    }

    // Update is called once per frame
    void Update()
    {
        debug.SetText(player.data.id + "\n" + player.data.username + "\n" + player.data.email);
        //Debug.Log("username: "+User.username);
    }

    void UpdateUser()
    {
        StartCoroutine(GetUser(UserManagement.GetToken()));
    }

    private IEnumerator GetUser(string token)
    {
        WWWForm form = new WWWForm();

        form.AddField("jwt", token);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/testapi/api/validate-token.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                player = JsonUtility.FromJson<ValidateUser>(www.downloadHandler.text);

                Debug.Log(player.data.username);

                //userData = JsonUtility.FromJson<User>(ValidateUser.data);
            }
        }
    }
}
