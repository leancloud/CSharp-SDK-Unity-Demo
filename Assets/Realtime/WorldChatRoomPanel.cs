using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LeanCloud.Realtime;

public class WorldChatRoomPanel : MonoBehaviour {
    public GameObject contentObject;

    public Text messagePrefab;

    public void AddMessage(LCIMTextMessage textMessage) {
        Text message = Instantiate(messagePrefab).GetComponent<Text>();
        message.text = $"{textMessage.FromClientId}: {textMessage.Text}";
        message.transform.parent = contentObject.transform;
    }
}
