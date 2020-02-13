using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class QuestMenu : MonoBehaviour
{
    public string questId;
    bool isDone = false;

    // get global data
    public UserData userData;
    public QuestManager questManager;
    private GroupQuest[] groupQuests;

    // retrive base url from API class
    private string url = API.url;

    // Object collider, it will be the radius for player to pick up item from this object
    Collider radius;

    // If player in radius
    bool inRadius = false;

    // Local player avatar
    GameObject player;

    // == GUI stuff ==
    // Control hint prefab
    public GameObject controlHintUI, panelQuest;
    GameObject controlHintUIActive;
    public TMP_Text textObjective, textTask, textTitle;
    public Button btnAccept;

    // Quest info
    Quest quest = new Quest();

    // Start is called before the first frame update
    void Start()
    {
        radius = this.GetComponent<Collider>();

        groupQuests = null;

        GetQuest(questId);

        //Debug.Log(userData.user.group.id);

        RetrieveGroupQuest();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get local player avatar
        player = GameObject.Find("Local");

        /*if(!String.IsNullOrEmpty( userData.user.group.id ))
        {
            RetrieveGroupQuest(userData.user.group.id);
        }*/

        if(groupQuests != null)
        {
            foreach (GroupQuest gQ in groupQuests)
            {
                if (gQ.quest.id == questId)
                {
                    isDone = true;
                }
            }
        }

        // If player in radius and E key is pressed, show quest window
        if (inRadius && Input.GetKeyDown(KeyCode.E) && !isDone)
        {
            // remove E to interact message
            Destroy(controlHintUIActive);

            // assign accept button functionality
            btnAccept.onClick.AddListener( () => Accept(questId) );

            // set text before showing quest window
            textTitle.SetText(quest.title);
            textObjective.SetText(quest.description);
            // convert array to string
            string taskList = "";
            foreach(Task task in quest.tasks)
            {
                taskList += task.task + "\n";
            }
            textTask.SetText(taskList);

            // show quest window
            panelQuest.gameObject.SetActive(true);
        }
    }

    public void Accept(string questId)
    {
        StartCoroutine(StartQuest(questId, UserManagement.GetToken()));
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
            else
            {
                panelQuest.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // when collide with local player
        if (other.name == player.name && !String.IsNullOrEmpty(userData.user.group.id) && !isDone) //userData.user.group == null
        {
            Debug.Log(userData.user.group.id);
            controlHintUIActive = Instantiate(controlHintUI, FindObjectOfType<Canvas>().transform);
            inRadius = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // when collide with local player
        if (other.name == player.name)
        {
            Destroy(controlHintUIActive);
            inRadius = false;
        }
    }
    
    private void GetQuest(string questId)
    {
        StartCoroutine(ReadQuestData(questId));
    }

    private IEnumerator ReadQuestData(string questId)
    {
        WWWForm form = new WWWForm();

        form.AddField("id", questId);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "get-quest.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                quest = JsonUtility.FromJson<Quest>(www.downloadHandler.text);
                //tasks = quest.tasks;

                //Debug.Log(quest.title + "\n" + quest.tasks[0].task + "\n" + quest.tasks[0].hint);
            }
        }
    }

    public void RetrieveGroupQuest()
    {
        StartCoroutine(GetGroupQuest());
    }

    private IEnumerator GetGroupQuest()
    {
        while(true)
        {
            if (!String.IsNullOrEmpty(userData.user.group.id))
            {
                WWWForm form = new WWWForm();

                form.AddField("groupId", userData.user.group.id);

                using (UnityWebRequest www = UnityWebRequest.Post(url + "get-quest-group.php", form))
                {
                    yield return www.SendWebRequest();

                    //yield return null;

                    if (www.isNetworkError || www.isHttpError)
                    {
                    }
                    else
                    {
                        GroupQuests gQ = JsonUtility.FromJson<GroupQuests>(www.downloadHandler.text);

                        groupQuests = gQ.groupQuests;
                    }
                }
            }
            else
            {
                yield return null;
            }
        }
    }
}
