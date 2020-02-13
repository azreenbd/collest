using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class DuctTape : MonoBehaviour
{
    // get user data
    public UserData userData;

    // retrive base url from API class
    private string url = API.url;

    // ID of item in database
    const string id_DuctTape = "p0003";

    // Object collider, it will be the radius for player to pick up item from this object
    Collider radius;

    // If player in radius
    bool inRadius = false;

    // Local player avatar
    GameObject player;

    // == GUI stuff ==
    // Control hint prefab
    public GameObject controlHintUI;
    GameObject controlHintUIActive;
    // message box
    public GameObject messageBox;
    public TMP_Text messageBoxText;
    private string message;

    // Start is called before the first frame update
    void Start()
    {
        radius = this.GetComponent<Collider>();
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
            // Hide "E to search" message
            Destroy(controlHintUIActive);

            AddItem(id_DuctTape, 1);
            message += "1x Duct Tape\n";

            StartCoroutine(ShowMessageBox(2.0f));
        }
    }

    private IEnumerator ShowMessageBox(float duration)
    {
        // display item that is added to user inventory
        messageBox.gameObject.SetActive(true);
        messageBoxText.SetText(message);

        // delay time
        yield return new WaitForSeconds(duration);

        // hide message box
        message = "";
        messageBoxText.SetText(message);
        messageBox.gameObject.SetActive(false);
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
}
