using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LeanCloud;
using LeanCloud.Realtime;

public class ChatScrollView : MonoBehaviour {
    public GameObject contentObject;

    public Text messagePrefab;

    public async void AddMessage(LCIMTextMessage textMessage) {
        try {
            Hero from = await Realtime.GetHeroById(textMessage.FromClientId);
            AddMessage($"{from.Name} : {textMessage.Text}");
        } catch (LCException e) {
            Debug.LogError(e);
        }
    }

    public void AddSentPrivateMessage(string name, LCIMTextMessage textMessage) {
        AddMessage($"你 -> {name} : {textMessage.Text}");
    }

    public async void AddReceivedPrivateMessage(LCIMTextMessage textMessage) {
        try {
            Hero from = await Realtime.GetHeroById(textMessage.FromClientId);
            AddMessage($"{from.Name} -> 你 : {textMessage.Text}");
        } catch (LCException e) {
            Debug.LogError(e);
        }
    }

    private void AddMessage(string text) {
        Text message = Instantiate(messagePrefab).GetComponent<Text>();
        message.text = text;
        message.transform.SetParent(contentObject.transform);
    }
}
