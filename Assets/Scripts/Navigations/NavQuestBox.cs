using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavQuestBox : MonoBehaviour
{

    public void AcceptClick()
    {
        
    }

    public void CancelClick()
    {
        // hide quest window
        this.gameObject.SetActive(false);
    }
}
