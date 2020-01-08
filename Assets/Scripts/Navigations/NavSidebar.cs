using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavSidebar : MonoBehaviour
{
    public UserData user;

    public GameObject panelGroup, panelQuest, panelInventory;
    public GameObject panelNoGroup, panelHasGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void showGroupClick()
    {
        panelGroup.gameObject.SetActive(true);
        if (string.IsNullOrEmpty(user.group))
        {
            panelNoGroup.gameObject.SetActive(true);
            panelHasGroup.gameObject.SetActive(false);
        }
        else
        {
            panelNoGroup.gameObject.SetActive(false);
            panelHasGroup.gameObject.SetActive(true);
        }
    }

}
