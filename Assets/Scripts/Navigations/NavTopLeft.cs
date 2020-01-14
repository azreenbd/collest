using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavTopLeft : MonoBehaviour
{
    public void LogoutClick()
    {
        UserManagement.SetToken(null);
    }
}
