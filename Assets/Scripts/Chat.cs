using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chat : MonoBehaviour
{
    /************ obselete ***********/
    public TMP_InputField messageBox;

    public List<string> chatHistory = new List<string>();

    private string newMessage = string.Empty;

    private void OnGUI()
    {
        GUILayout.BeginHorizontal(GUILayout.Width(250));

        foreach(string message in chatHistory)
        {
            GUILayout.Label(message);
        }
    }

    public void SendClick()
    {
        newMessage = messageBox.text.Trim();
        if (!string.IsNullOrEmpty(newMessage))
        {
            //networkView.RPC("Chat Message", RPCMode.AllBuffered, new object[] { newMessage } );
            newMessage = string.Empty;
            
        }
    }

}
