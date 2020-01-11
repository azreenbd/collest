using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavSidebar : MonoBehaviour
{
    public UserData userData;

    public GameObject panelGroup, panelQuest, panelInventory;
    public GameObject panelNoGroup, panelHasGroup, panelSubGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void showGroupClick()
    {
        panelGroup.gameObject.SetActive(true);
        if (string.IsNullOrEmpty(userData.user.group.id))
        {
            panelNoGroup.gameObject.SetActive(true);
            panelHasGroup.gameObject.SetActive(false);
            panelSubGroup.gameObject.SetActive(false);
        }
        else
        {
            panelNoGroup.gameObject.SetActive(false);
            panelHasGroup.gameObject.SetActive(true);
            panelSubGroup.gameObject.SetActive(false);
        }
    }

}
