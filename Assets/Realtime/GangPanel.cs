using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using LeanCloud.Realtime;

public class GangPanel : MonoBehaviour {
    public GameObject gangListPanel;

    public GangChatPanel gangChatPanel;

    async void Start() {
        gangListPanel.SetActive(false);
        gangChatPanel.gameObject.SetActive(false);

        LCIMConversationQuery query = new LCIMConversationQuery(Realtime.Client);
        query.WhereEqualTo("gang", true)
            .WhereEqualTo("m", Realtime.Client.Id);
        ReadOnlyCollection<LCIMConversation> gangConversations = await query.Find();
        if (gangConversations?.Count > 0) {
            // 已加入行会
            LCIMConversation gangConv = gangConversations[0];
            gangChatPanel.SetConversation(gangConv);
            gangChatPanel.gameObject.SetActive(true);
        } else {
            // 未加入行会
            gangListPanel.SetActive(true);
        }
    }

    private void OnCreateOrJoinGang(LCIMConversation gangConv) {
        gangListPanel.SetActive(false);
        gangChatPanel.SetConversation(gangConv);
        gangChatPanel.gameObject.SetActive(true);
    }

    private void OnQuitGang() {
        gangListPanel.SetActive(true);
        gangChatPanel.gameObject.SetActive(false);
    }
}
