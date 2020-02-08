using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class QuestTracker : MonoBehaviour
{
    [SerializeField]
    private GameObject questInfoPrefab;

    public GameObject panelQuestList, panelNoQuest;
    public UserData userData;

    private GroupQuest[] quests;
    private InventoryItem[] groupItems;

    List<GameObject> questInfo = new List<GameObject>();

    string url = API.url;

    // Start is called before the first frame update
    void Start()
    {
        panelQuestList.gameObject.SetActive(false);
        panelNoQuest.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(!String.IsNullOrEmpty( userData.user.group.id ))
        {
            RetrieveGroupInventory(userData.user.group.id);
            RetrieveGroupQuest(userData.user.group.id);
        }
        else
        {
            panelQuestList.gameObject.SetActive(false);
            panelNoQuest.gameObject.SetActive(true);
        }
        
    }

    void ListQuest()
    {
        foreach (GameObject qI in questInfo) {
            Destroy(qI);
        }

        questInfo.Clear();

        foreach (GroupQuest quest in quests)
        {
            if(!quest.isComplete)
            {
                questInfo.Add(Instantiate(questInfoPrefab) as GameObject);

                int index = questInfo.Count - 1;

                questInfo[index].SetActive(true);

                questInfo[index].GetComponent<QuestStatusUI>().SetTitle(quest.quest.title);
                questInfo[index].GetComponent<QuestStatusUI>().SetTask(quest.quest.tasks, groupItems);

                questInfo[index].transform.SetParent(questInfoPrefab.transform.parent, false);
            }
        }

        questInfoPrefab.SetActive(false);
    }

    public void RetrieveGroupQuest(string groupId)
    {
        StartCoroutine(GetGroupQuest(groupId));
    }

    private IEnumerator GetGroupQuest(string groupId)
    {
        WWWForm form = new WWWForm();

        form.AddField("groupId", groupId);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "get-quest-group.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);

                panelQuestList.gameObject.SetActive(false);
                panelNoQuest.gameObject.SetActive(true);
            }
            else
            {
                panelQuestList.gameObject.SetActive(true);
                panelNoQuest.gameObject.SetActive(false);

                GroupQuests groupQuests = JsonUtility.FromJson<GroupQuests>(www.downloadHandler.text);

                quests = groupQuests.groupQuests;

                ListQuest();
            }
        }
    }

    public void RetrieveGroupInventory(string groupId)
    {
        StartCoroutine(GetGroupInventory(groupId));
    }

    private IEnumerator GetGroupInventory(string groupId)
    {
        WWWForm form = new WWWForm();

        form.AddField("groupId", groupId);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "get-inventory-group.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                GroupInventory groupInventory = JsonUtility.FromJson<GroupInventory>(www.downloadHandler.text);

                groupItems = groupInventory.items;
            }
        }
    }
}
