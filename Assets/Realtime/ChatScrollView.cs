using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LeanCloud.Realtime;

public class ChatScrollView : MonoBehaviour {
    public GameObject contentObject;

    public Text messagePrefab;

    public void AddMessage(LCIMTextMessage textMessage) {
        AddMessage($"{textMessage.FromClientId}: {textMessage.Text}");
    }

    public void AddPrivateMessage(string fromId, string toId, LCIMTextMessage textMessage) {
        AddMessage($"{fromId} -> {toId} : {textMessage.Text}");
    }

    private void AddMessage(string text) {
        Text message = Instantiate(messagePrefab).GetComponent<Text>();
        message.text = text;
        message.transform.SetParent(contentObject.transform);
    }
}
