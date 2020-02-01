using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class QuestManager : MonoBehaviour
{
    // retrieve base url from API class
    private string url = API.url;

    private string jwt = UserManagement.GetToken();

    public void Accept(string questId)
    {
        StartCoroutine(StartQuest(questId, jwt));
    }

    private IEnumerator StartQuest(string questId, string jwt)
    {
        WWWForm form = new WWWForm();

        form.AddField("questId", questId);
        form.AddField("jwt", jwt);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "start-quest.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
        }
    }
}
