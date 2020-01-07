using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManagement : MonoBehaviour
{
    private string jwt = UserManagement.GetToken();
    public void CreateGroup(string groupName)
    {
        Debug.Log("Group: " + groupName + "\nJWT: " + jwt);
    }
}
