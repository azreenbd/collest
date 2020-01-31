using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Quest1 : NetworkBehaviour
{
    string questId = "q001";

    // get user data
    public UserData userData;

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
    public TMP_Text textObjective, textTask;

    // Quest info
    string objective;
    // task and hint
    string[,] task;

    // Start is called before the first frame update
    void Start()
    {
        radius = this.GetComponent<Collider>();

        objective = "To learn the law of ... by doing...";

        // task and hint
        task = new string[,] {
            { "Find 1 water bottle", "Take a look at a recyling bin or shop." },
            { "Find 1 air pump", "Scattered somewhere around the plaza."},
            { "Find 1 duct tape", "Take a look at a recyling bin or shop." }
        };
    }

    // Update is called once per frame
    void Update()
    {
        // Get local player avatar
        player = GameObject.Find("Local");

        // If player in radius and E key is pressed
        // Give player random item
        if (inRadius && Input.GetKeyDown(KeyCode.E))
        {
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // when collide with local player
        if (other.name == player.name)
        {
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
    /*
    private void AddItem(string itemId, int amount)
    {
        StartCoroutine(AddToInventory(itemId, amount));
    }

    private IEnumerator AddToInventory(string itemId, int amount)
    {
        WWWForm form = new WWWForm();

        form.AddField("userId", userData.user.id);
        form.AddField("itemId", itemId);
        form.AddField("amount", amount);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "add-inventory.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
        }
    }

    private string GetItemName(string itemId)
    {
        switch (itemId)
        {
            case id_Bottle:
                return "Water Bottle";
            case id_Paper:
                return "Paper";
            case id_PlasticBag:
                return "Plastic Bag";
            default:
                return "";
        }
    }*/
}
