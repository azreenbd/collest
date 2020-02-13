using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InventoryMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject itemInfoPrefab;

    public GameObject panelItemList, panelNoItem;
    public UserData userData;

    private InventoryItem[] userItems;
    List<GameObject> userItemThumbs = new List<GameObject>();

    string url = API.url;

    // Start is called before the first frame update
    void Start()
    {
        panelItemList.gameObject.SetActive(false);
        panelNoItem.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //RetrieveInventory(userData.user.id);

        if(userItems == null)
        {
            panelItemList.gameObject.SetActive(false);
            panelNoItem.gameObject.SetActive(true);
        }
        else
        {
            ListItem();

            panelItemList.gameObject.SetActive(true);
            panelNoItem.gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        StopCoroutine(GetInventory());
    }

    void OnEnable()
    {
        StartCoroutine(GetInventory());
    }

    void ListItem()
    {
        foreach (GameObject uIT in userItemThumbs)
        {
            Destroy(uIT);
        }

        userItemThumbs.Clear();

        foreach (InventoryItem item in userItems)
        {
            userItemThumbs.Add(Instantiate(itemInfoPrefab) as GameObject);

            int index = userItemThumbs.Count - 1;

            userItemThumbs[index].SetActive(true);
            userItemThumbs[index].GetComponent<InventoryThumbnailUI>().SetItemInfo(item);

            userItemThumbs[index].transform.SetParent(itemInfoPrefab.transform.parent, false);
        }

        itemInfoPrefab.SetActive(false);
    }

    public void RetrieveInventory()
    {
        StartCoroutine(GetInventory());
    }

    private IEnumerator GetInventory()
    {
        while(true)
        {
            WWWForm form = new WWWForm();

            form.AddField("userId", userData.user.id);

            using (UnityWebRequest www = UnityWebRequest.Post(url + "get-inventory.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    userItems = null;
                }
                else
                {
                    GroupInventory userInventory = JsonUtility.FromJson<GroupInventory>(www.downloadHandler.text);

                    userItems = userInventory.items;
                }
            }
        }
    }
}
