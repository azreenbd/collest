using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class InventoryThumbnailUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text txtItem, txtAmount;

    Item itemInfo;

    string url = API.url;

    public void SetItemInfo(InventoryItem item)
    {
        //this.txtItem.SetText(item.name);
       
        this.txtItem.SetText(GetName(item.itemId));
        this.txtAmount.SetText("x" + item.amount.ToString());
    }

    private string GetName(string itemId)
    {
        switch (itemId)
        {
            case "p0001":
                return "Water Bottle";
            case "p0002":
                return "Air Pump";
            case "p0003":
                return "Duct Tape";
            case "p0004":
                return "Paper";
            case "p0005":
                return "Plastic Bag";
            case "p0006":
                return "Drinking Glass";
            case "p0007":
                return "Vegetable Oil";
            case "p0008":
                return "Salt";
            default:
                return "<Unknown Item>";
        }
    }

    
}
