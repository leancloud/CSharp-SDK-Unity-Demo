using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using LeanCloud.Realtime;
using LeanCloud;

public class GangChatPanel : MonoBehaviour {
    public Text nameText;

    public ChatScrollView chatScrollView;
    public InputField messageInputField;

    private LCIMConversation gangConversation;

    public void SetConversation(LCIMConversation conversation) {
        gangConversation = conversation;
        nameText.text = conversation.Name;   
    }

    void OnEnable() {
        Realtime.Client.OnMessage += OnMessage;
    }

    void OnDisable() {
        Realtime.Client.OnMessage -= OnMessage;
    }

    public async void OnQuitClicked() {
        try {
            await gangConversation.Quit();
            SendMessageUpwards("OnQuitGang");
        } catch (LCException e) {
            Debug.LogError($"{e.Code} - {e.Message}");
        }
    }

    public async void OnSendClicked() {
        string text = messageInputField.text;
        if (string.IsNullOrEmpty(text)) {
            return;
        }

        LCIMTextMessage message = new LCIMTextMessage(text);
        await gangConversation.Send(message);
        chatScrollView.AddMessage(message);
    }

    private void OnMessage(LCIMConversation conversation, LCIMMessage message) {
        if (gangConversation.Id == conversation.Id &&
            message is LCIMTextMessage textMessage) {
            _ = conversation.Read();
            chatScrollView.AddMessage(textMessage);
        }
    }
}
