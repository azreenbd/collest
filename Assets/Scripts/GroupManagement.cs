using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GroupManagement : MonoBehaviour
{
    // retrieve base url from API class
    private string url = API.url;
    private User member = new User();

    private string jwt = UserManagement.GetToken();

    public void Create(string groupName)
    {
        StartCoroutine(MakeGroup(groupName));
    }
    public void Disband(string groupId)
    {
        StartCoroutine(DeleteGroup(groupId));
    }
    public void Join(string groupId)
    {
        StartCoroutine(JoinGroup(groupId));
    }
    public void AddMember(string username)
    {
        StartCoroutine(GetMember(username));
    }
    public void Leave()
    {
        StartCoroutine(LeaveGroup());
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

        form.AddField("id", groupId);
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
    private IEnumerator JoinGroup(string groupId)
    {
        WWWForm form = new WWWForm();

        form.AddField("id", groupId);
        form.AddField("jwt", this.jwt);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "join-group.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Group joined");
            }
        }
    }
    private IEnumerator AddGroupMember(string userId)
    {
        WWWForm form = new WWWForm();

        form.AddField("id", userId);
        form.AddField("jwt", UserManagement.GetToken());

        using (UnityWebRequest www = UnityWebRequest.Post(url + "add-member.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Member added.");
            }
        }
    }
    private IEnumerator GetMember(string username)
    {
        WWWForm form = new WWWForm();

        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "get-user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                member = JsonUtility.FromJson<User>(www.downloadHandler.text);
                StartCoroutine(AddGroupMember(member.id));
            }
        }
        
    }
    private IEnumerator LeaveGroup()
    {
        WWWForm form = new WWWForm();

        form.AddField("jwt", this.jwt);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "leave-group.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Group left.");
            }
        }
    }


}
