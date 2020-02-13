using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class QuestStatusUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text title, textTask;

    public GameObject task, tick, btnDone, panelQuestDone;
    List<GameObject> taskboxs = new List<GameObject>();

    Quest quest;
    string groupId;


    string url = API.url;

    public void SetTitle(string title)
    {
        this.title.SetText(title);
    }

    public void SetTask(Quest quest, InventoryItem[] items, string groupId)
    {
        int totalTask = 0;
        int totalTaskDone = 0;

        this.quest = quest;
        this.groupId = groupId;

        foreach(Task t in quest.tasks)
        {
            //this.textTask.SetText(t.task);
            GameObject taskbox = Instantiate(task) as GameObject;
            taskbox.GetComponentInChildren<TMP_Text>().SetText(t.task);
            taskbox.SetActive(true);
            taskbox.transform.SetParent(task.transform.parent, false);

            totalTask++;

            if(items != null)
            {
                foreach (InventoryItem iI in items)
                {
                    if (iI.itemId == t.itemId && iI.amount >= t.itemAmount)
                    {
                        GameObject checkmark = Instantiate(tick) as GameObject;
                        checkmark.SetActive(true);
                        checkmark.transform.SetParent(taskbox.transform, false);

                        totalTaskDone++;
                    }
                }
            }

            taskboxs.Add(taskbox);
        }

        if (totalTask == totalTaskDone)
        {
            GameObject doneBtn = Instantiate(btnDone) as GameObject;
            doneBtn.SetActive(true);
            doneBtn.transform.SetParent(btnDone.transform.parent, false);

            doneBtn.GetComponent<Button>().onClick.AddListener( () => BtnDoneClick(groupId, quest.id));
        }

        task.SetActive(false);
        btnDone.SetActive(false);
    }

    public void Refresh(InventoryItem[] items)
    {
        int totalTask = 0;
        int totalTaskDone = 0;

        foreach(GameObject tb in taskboxs)
        {
            Destroy(tb);
        }

        foreach (Task t in this.quest.tasks)
        {
            //this.textTask.SetText(t.task);
            GameObject taskbox = Instantiate(task) as GameObject;
            taskbox.GetComponentInChildren<TMP_Text>().SetText(t.task);
            taskbox.SetActive(true);
            taskbox.transform.SetParent(task.transform.parent, false);

            totalTask++;

            if (items != null)
            {
                foreach (InventoryItem iI in items)
                {
                    if (iI.itemId == t.itemId && iI.amount >= t.itemAmount)
                    {
                        GameObject checkmark = Instantiate(tick) as GameObject;
                        checkmark.SetActive(true);
                        checkmark.transform.SetParent(taskbox.transform, false);

                        totalTaskDone++;
                    }
                }
            }

            taskboxs.Add(taskbox);
        }

        if (totalTask == totalTaskDone)
        {
            GameObject doneBtn = Instantiate(btnDone) as GameObject;
            doneBtn.SetActive(true);
            doneBtn.transform.SetParent(btnDone.transform.parent, false);

            doneBtn.GetComponent<Button>().onClick.AddListener(() => BtnDoneClick(this.groupId, this.quest.id));
        }

        task.SetActive(false);
        btnDone.SetActive(false);
    }

    public void BtnDoneClick(string groupId, string questId)
    {
        //Debug.Log("g: " + groupId + " q: " + questId);
        StartCoroutine(SetQuestComplete(groupId, questId));

        panelQuestDone.gameObject.SetActive(true);
    }

    private IEnumerator SetQuestComplete(string groupId, string questId)
    {
        WWWForm form = new WWWForm();

        form.AddField("groupId", groupId);
        form.AddField("questId", questId);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "complete-quest.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) { } else { Debug.Log("Quest complete"); };
        }
    }
}
