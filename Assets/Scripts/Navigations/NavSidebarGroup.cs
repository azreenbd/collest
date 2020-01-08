using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NavSidebarGroup : MonoBehaviour
{
    GroupManagement groupManager;

    public UserData user;
    public GameObject panelGroup, panelCreateGroup, panelEditGroup, panelNoGroup;

    public TMP_InputField groupNameCreate, groupNameEdit;

    private bool hasGroup = false;

    // Start is called before the first frame update
    void Start()
    {
        groupManager = GetComponent<GroupManagement>();

    }

    // can remove later, cause group are not suppose to appear on start, so UserData have time to assign data
    void Update()
    {
        /*Debug.Log(user.group);
        if (string.IsNullOrEmpty(user.group) || user.group == "")
        {
            panelCreateGroup.gameObject.SetActive(false);
            panelEditGroup.gameObject.SetActive(false);
            panelGroup.gameObject.SetActive(false);
            panelNoGroup.gameObject.SetActive(true);
        }
        else
        {
            panelCreateGroup.gameObject.SetActive(false);
            panelEditGroup.gameObject.SetActive(false);
            panelGroup.gameObject.SetActive(true);
            panelNoGroup.gameObject.SetActive(false);
        }*/
    }

    public void CreateGroupClick()
    {
        panelCreateGroup.gameObject.SetActive(true);
        panelEditGroup.gameObject.SetActive(false);
        panelGroup.gameObject.SetActive(false);
        panelNoGroup.gameObject.SetActive(false);
    }

    public void ConfirmCreateGroupClick()
    {
        // DO web api here, if response OK, display group panel
        groupManager.Create(groupNameCreate.text);

        // display panel group
        panelCreateGroup.gameObject.SetActive(false);
        panelEditGroup.gameObject.SetActive(false);
        panelGroup.gameObject.SetActive(true);
        panelNoGroup.gameObject.SetActive(false);

        // else error message

    }

    public void CancelCreateGroupClick()
    {
        // display "no group" message again
        panelCreateGroup.gameObject.SetActive(false);
        panelEditGroup.gameObject.SetActive(false);
        panelGroup.gameObject.SetActive(false);
        panelNoGroup.gameObject.SetActive(true);
    }

    public void EditGroupClick()
    {
        // display panel group
        panelCreateGroup.gameObject.SetActive(false);
        panelEditGroup.gameObject.SetActive(true);
        panelGroup.gameObject.SetActive(false);
        panelNoGroup.gameObject.SetActive(false);
    }

    public void ConfirmEditGroupClick()
    {
        // DO web api here, if response OK, display group panel

        // display panel group
        panelCreateGroup.gameObject.SetActive(false);
        panelEditGroup.gameObject.SetActive(false);
        panelGroup.gameObject.SetActive(true);
        panelNoGroup.gameObject.SetActive(false);

        // else error message


    }

    public void DisbandGroupClick()
    {
        // DO group deletion here!!!
        groupManager.Disband(user.group);



        // display no group message
        panelCreateGroup.gameObject.SetActive(false);
        panelEditGroup.gameObject.SetActive(false);
        panelGroup.gameObject.SetActive(false);
        panelNoGroup.gameObject.SetActive(true);
    }
}
