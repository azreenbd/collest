using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NavSidebarGroup : MonoBehaviour
{
    GroupManagement groupManager;

    public UserData userData;
    public GameObject panelGroup, panelNoGroup, panelSubGroup, panelCreateGroup, 
        panelEditGroup, panelJoinGroup, panelAddMember, 
        btnEditGroup, btnLeaveGroup;

    public TMP_InputField inputGroupNameCreate, inputGroupNameEdit, inputGroupIdJoin, inputUsernameAdd;
    public TMP_Text textGroupName;

    private bool hasGroup = false;
    private bool isCreator = false;

    // Start is called before the first frame update
    void Start()
    {
        groupManager = GetComponent<GroupManagement>();

        btnEditGroup.gameObject.SetActive(false);
        btnLeaveGroup.gameObject.SetActive(false);
    }

    // can remove later, cause group are not suppose to appear on start, so UserData have time to assign data
    void Update()
    {
        hasGroup = !string.IsNullOrEmpty(userData.user.group.id);
        isCreator = userData.user.group.creator == userData.user.id ? true : false;

        if(hasGroup)
        {
            textGroupName.SetText(userData.user.group.name);
        }

        if(isCreator)
        {
            btnEditGroup.gameObject.SetActive(true);
            btnLeaveGroup.gameObject.SetActive(false);
        }
        else
        {
            btnEditGroup.gameObject.SetActive(false);
            btnLeaveGroup.gameObject.SetActive(true);
        }
    }

    public void ShowGroupHome()
    {
        // show NoGroup when user don't have a group
        if (string.IsNullOrEmpty(userData.user.group.id))
        {
            panelNoGroup.gameObject.SetActive(true);
            panelGroup.gameObject.SetActive(false);
            panelSubGroup.gameObject.SetActive(false);
        }
        else
        {
            panelNoGroup.gameObject.SetActive(false);
            panelGroup.gameObject.SetActive(true);
            panelSubGroup.gameObject.SetActive(false);
        }
    }

    public void CreateGroupClick()
    {
        panelSubGroup.gameObject.SetActive(true);
        panelCreateGroup.gameObject.SetActive(true);
        panelEditGroup.gameObject.SetActive(false);
        panelGroup.gameObject.SetActive(false);
        panelNoGroup.gameObject.SetActive(false);
        panelJoinGroup.gameObject.SetActive(false);
        panelAddMember.gameObject.SetActive(false);
    }

    public void ConfirmCreateGroupClick()
    {
        // DO web api here, if response OK, display group panel
        groupManager.Create(inputGroupNameCreate.text);

        // display panel group
        panelSubGroup.gameObject.SetActive(false);
        panelGroup.gameObject.SetActive(true);
        panelNoGroup.gameObject.SetActive(false);

        // else error message

    }

    public void CancelCreateGroupClick()
    {
        // display "no group" message again
        ShowGroupHome();
    }

    public void EditGroupClick()
    {
        //inputGroupNameEdit.text("ssss");

        // display panel group
        panelSubGroup.gameObject.SetActive(true);
        panelCreateGroup.gameObject.SetActive(false);
        panelEditGroup.gameObject.SetActive(true);
        panelGroup.gameObject.SetActive(false);
        panelNoGroup.gameObject.SetActive(false);
        panelJoinGroup.gameObject.SetActive(false);
        panelAddMember.gameObject.SetActive(false);
    }

    public void ConfirmEditGroupClick()
    {
        // DO web api here, if response OK, display group panel

        // display panel group
        ShowGroupHome();

        // else error message


    }

    public void DisbandGroupClick()
    {
        // DO group deletion here!!!
        groupManager.Disband(userData.user.group.id);


        // display no group message
        panelSubGroup.gameObject.SetActive(false);
        panelGroup.gameObject.SetActive(false);
        panelNoGroup.gameObject.SetActive(true);
    }

    public void LeaveGroupClick()
    {
        // DO group deletion here!!!
        groupManager.Leave();

        // display no group message
        panelSubGroup.gameObject.SetActive(false);
        panelGroup.gameObject.SetActive(false);
        panelNoGroup.gameObject.SetActive(true);
    }

    public void JoinGroupClick()
    {
        panelSubGroup.gameObject.SetActive(true);
        panelCreateGroup.gameObject.SetActive(false);
        panelEditGroup.gameObject.SetActive(false);
        panelGroup.gameObject.SetActive(false);
        panelNoGroup.gameObject.SetActive(false);
        panelJoinGroup.gameObject.SetActive(true);
        panelAddMember.gameObject.SetActive(false);
    }

    public void ConfirmJoinGroupClick()
    {
        groupManager.Join(inputGroupIdJoin.text);

        panelSubGroup.gameObject.SetActive(false);
        panelGroup.gameObject.SetActive(true);
        panelNoGroup.gameObject.SetActive(false);
    }

    public void AddMemberClick()
    {
        panelSubGroup.gameObject.SetActive(true);
        panelCreateGroup.gameObject.SetActive(false);
        panelEditGroup.gameObject.SetActive(false);
        panelGroup.gameObject.SetActive(false);
        panelNoGroup.gameObject.SetActive(false);
        panelJoinGroup.gameObject.SetActive(false);
        panelAddMember.gameObject.SetActive(true);
    }

    public void ConfirmAddMemberClick()
    {
        groupManager.AddMember(inputUsernameAdd.text);

        panelSubGroup.gameObject.SetActive(false);
        panelGroup.gameObject.SetActive(true);
        panelNoGroup.gameObject.SetActive(false);
    }
}
