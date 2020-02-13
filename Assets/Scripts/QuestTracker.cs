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

    int totalQuest = 0;
    int totalQuestOld = 0;

    string url = API.url;

    // Start is called before the first frame update
    void Start()
    {
        panelQuestList.gameObject.SetActive(false);
        panelNoQuest.gameObject.SetActive(false);

        /*RetrieveGroupInventory();
        RetrieveGroupQuest();*/
    }

    void OnDisable()
    {
        StopCoroutine(GetGroupInventory());
        StopCoroutine(GetGroupQuest());
    }

    void OnEnable()
    {
        StartCoroutine(GetGroupInventory());
        StartCoroutine(GetGroupQuest());
    }

    void ListQuest()
    {
        if (quests.Length > totalQuest)
        {
            foreach (GameObject qI in questInfo)
            {
                Destroy(qI);
            }

            questInfo.Clear();

            foreach (GroupQuest quest in quests)
            {
                if (!quest.isComplete)
                {
                    questInfo.Add(Instantiate(questInfoPrefab) as GameObject);

                    int index = questInfo.Count - 1;

                    questInfo[index].SetActive(true);
                    questInfo[index].GetComponent<QuestStatusUI>().SetTitle(quest.quest.title);
                    questInfo[index].GetComponent<QuestStatusUI>().SetTask(quest.quest, groupItems, userData.user.group.id);

                    questInfo[index].transform.SetParent(questInfoPrefab.transform.parent, false);
                }
            }
        }
        else
        {
            foreach (GameObject qI in questInfo)
            {
                //refresh task only
                qI.GetComponent<QuestStatusUI>().Refresh(groupItems);
            }
        }

        totalQuest = quests.Length;

        questInfoPrefab.SetActive(false);
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

                    if (www.isNetworkError || www.isHttpError)
                    {
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
            else
            {
                panelQuestList.gameObject.SetActive(false);
                panelNoQuest.gameObject.SetActive(true);

                yield return null;
            }
            
        }
    }

    public void RetrieveGroupInventory()
    {
        StartCoroutine(GetGroupInventory());
    }

    private IEnumerator GetGroupInventory()
    {
        while(true)
        {
            if (!String.IsNullOrEmpty(userData.user.group.id))
            {
                WWWForm form = new WWWForm();

                form.AddField("groupId", userData.user.group.id);

                using (UnityWebRequest www = UnityWebRequest.Post(url + "get-inventory-group.php", form))
                {
                    yield return www.SendWebRequest();

                    if (www.isNetworkError || www.isHttpError)
                    {
                        groupItems = null;
                    }
                    else
                    {
                        GroupInventory groupInventory = JsonUtility.FromJson<GroupInventory>(www.downloadHandler.text);

                        groupItems = groupInventory.items;
                    }
                }
            }
            else
            {
                panelQuestList.gameObject.SetActive(false);
                panelNoQuest.gameObject.SetActive(true);

                yield return null;
            }
            
        }
    }
}
