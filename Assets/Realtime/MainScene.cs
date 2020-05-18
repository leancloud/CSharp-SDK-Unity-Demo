using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using LeanCloud.Storage;
using LeanCloud.Realtime;

public class MainScene : MonoBehaviour {
    public InputField messageInputField;

    public WorldChatRoomPanel chatRoomPanel;

    private LCIMChatRoom chatRoom;

    // Start is called before the first frame update
    async void Start() {
        LCIMConversationQuery query = new LCIMConversationQuery(Realtime.Client);
        query.WhereEqualTo("tr", true)
            .WhereEqualTo("name", "World");
        chatRoom = (await query.Find()).First() as LCIMChatRoom;
        try {
            await chatRoom.Join();
            // TODO 激活世界聊天面板

            Realtime.Client.OnMessage += OnMessage;
        } catch (LCException e) {
            // TODO 加入聊天室失败
            Debug.LogError($"{e.Code}, {e.Message}");
        }
    }

    void OnDestroy() {
        Realtime.Client.OnMessage -= OnMessage;
    }

    public async void OnSendClicked() {
        string text = messageInputField.text;
        if (string.IsNullOrEmpty(text)) {
            return;
        }

        LCIMTextMessage message = new LCIMTextMessage(text);
        await chatRoom.Send(message);
        chatRoomPanel.AddMessage(message);
    }

    private void OnMessage(LCIMConversation conv, LCIMMessage message) {
        Debug.Log($"on message: {message.Id}");
        if (conv is LCIMChatRoom &&
            message is LCIMTextMessage textMessage) {
            chatRoomPanel.AddMessage(textMessage);
        }
    }
}
