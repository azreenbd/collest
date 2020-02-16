using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class NavShop : MonoBehaviour
{
    string url = API.url;

    public TMP_Text txtCoin;
    public UserData userData;

    void Update()
    {
        txtCoin.SetText("$" + userData.user.coin.ToString());
    }

    public void buyItemClick()
    {
        // hard code
        StartCoroutine(BuyItem("p0002"));
    }

    public void closeClick()
    {
        this.gameObject.SetActive(false);
    }

    private IEnumerator BuyItem(string itemId)
    {
        WWWForm form = new WWWForm();

        form.AddField("itemId", itemId);
        form.AddField("jwt", UserManagement.GetToken());

        using (UnityWebRequest www = UnityWebRequest.Post(url + "buy-item.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("fail to buy");
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
