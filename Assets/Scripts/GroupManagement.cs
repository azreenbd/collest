using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GroupManagement : MonoBehaviour
{
    // rest api url // maybe put this in one core file?
    private string url = "http://localhost/testapi/api/";

    private string jwt = UserManagement.GetToken();
    public void Create(string groupName)
    {
        //Debug.Log("Group: " + groupName + "\nJWT: " + jwt);
        StartCoroutine(MakeGroup(groupName));
    }
    public void Disband(string groupId)
    {
        //Debug.Log("Group: " + groupName + "\nJWT: " + jwt);
        StartCoroutine(DeleteGroup(groupId));
    }

    private IEnumerator MakeGroup(string groupName)
    {
        WWWForm form = new WWWForm();

        form.AddField("name", groupName);
        form.AddField("jwt", this.jwt);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "create-group.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Group created");
            }
        }
    }

    private IEnumerator DeleteGroup(string groupId)
    {
        WWWForm form = new WWWForm();

        form.AddField("name", groupId);
        form.AddField("jwt", this.jwt);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "delete-group.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Group deleted");
            }
        }
    }
}
