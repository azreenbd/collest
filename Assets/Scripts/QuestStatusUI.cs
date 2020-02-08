using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestStatusUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text title, textTask;

    public GameObject task, tick, btnDone;

    public void SetTitle(string title)
    {
        this.title.SetText(title);
    }

    public void SetTask(Quest quest, InventoryItem[] items, string groupId)
    {
        int totalTask = 0;
        int totalTaskDone = 0;

        foreach(Task t in quest.tasks)
        {
            this.textTask.SetText(t.task);
            GameObject taskbox = Instantiate(task) as GameObject;
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

    public void BtnDoneClick(string groupId, string questId)
    {
        Debug.Log("g: " + groupId + " q: " + questId);
    }
}
