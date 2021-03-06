﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using LeanCloud;
using LeanCloud.Realtime;

public class PrivateChatPanel : MonoBehaviour {
    // 对方 Id -> 对话
    private readonly Dictionary<string, LCIMConversation> privateConvs = new Dictionary<string, LCIMConversation>();

    public ChatScrollView chatScrollView;
    public InputField clientIdInputField;
    public InputField messageInputField;

    void Start() {
        Realtime.Client.OnMessage += OnMessage;
        Realtime.Client.OnMessageDelivered += OnMessageDelivered;
    }

    void OnDestroy() {
        Realtime.Client.OnMessage -= OnMessage;
        Realtime.Client.OnMessageDelivered -= OnMessageDelivered;
    }

    public async void OnSendClicked() {
        string name = clientIdInputField.text;
        if (string.IsNullOrEmpty(name)) {
            return;
        }
        string text = messageInputField.text;
        if (string.IsNullOrEmpty(text)) {
            return;
        }

        try {
            Hero hero = await Realtime.GetHeroByName(name);
            string clientId = hero.ObjectId;
            if (!privateConvs.TryGetValue(clientId, out LCIMConversation conv)) {
                conv = await Realtime.Client.CreateConversation(new string[] { clientId });
                privateConvs.Add(clientId, conv);
            }
            LCIMTextMessage message = new LCIMTextMessage(text);
            LCIMMessageSendOptions options = new LCIMMessageSendOptions {
                Receipt = true
            };
            await conv.Send(message, options);
            Debug.Log($"{message.Id} sent sucessfully");
            chatScrollView.AddSentPrivateMessage(hero.Name, message);
        } catch (LCException e) {
            Debug.LogError(e);
        }
    }

    private void OnMessage(LCIMConversation conversation, LCIMMessage message) {
        if (conversation.MemberIds.Count == 2 &&
            message is LCIMTextMessage textMessage) {
            _ = conversation.Read();
            chatScrollView.AddReceivedPrivateMessage(textMessage);
        }
    }

    private void OnMessageDelivered(LCIMConversation conv, string msgId) {
        Debug.Log($"{msgId} has delivered");
    }
}
