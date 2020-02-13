using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavSidebar : MonoBehaviour
{
    public UserData userData;

    public GameObject panelGroup, panelQuest, panelInventory;
    public GameObject panelNoGroup, panelHasGroup, panelSubGroup;

    bool inventoryIsActive = false;
    bool groupIsActive = false;
    bool questIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void showGroupClick()
    {
        if (groupIsActive)
        {
            panelGroup.gameObject.SetActive(false);
            panelQuest.gameObject.SetActive(false);
            panelInventory.gameObject.SetActive(false);

            groupIsActive = false;
            inventoryIsActive = false;
            questIsActive = false;
        }
        else
        {
            inventoryIsActive = false;
            questIsActive = false;
            groupIsActive = true;

            panelGroup.gameObject.SetActive(true);
            panelQuest.gameObject.SetActive(false);
            panelInventory.gameObject.SetActive(false);

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

    public void showQuestClick()
    {
        if(questIsActive)
        {
            panelQuest.gameObject.SetActive(false);
            panelGroup.gameObject.SetActive(false);
            panelInventory.gameObject.SetActive(false);

            groupIsActive = false;
            inventoryIsActive = false;
            questIsActive = false;
        }
        else
        {
            groupIsActive = false;
            inventoryIsActive = false;
            questIsActive = true;

            panelQuest.gameObject.SetActive(true);
            panelGroup.gameObject.SetActive(false);
            panelInventory.gameObject.SetActive(false);
        }
    }

    public void showInventoryClick()
    {
        if(inventoryIsActive)
        {
            panelQuest.gameObject.SetActive(false);
            panelGroup.gameObject.SetActive(false);
            panelInventory.gameObject.SetActive(false);

            groupIsActive = false;
            inventoryIsActive = false;
            questIsActive = false;
        }
        else
        {
            groupIsActive = false;
            questIsActive = false;
            inventoryIsActive = true;

            panelQuest.gameObject.SetActive(false);
            panelGroup.gameObject.SetActive(false);
            panelInventory.gameObject.SetActive(true);
        }
        
    }

}
