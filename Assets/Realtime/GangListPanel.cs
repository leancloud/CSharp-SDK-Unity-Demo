using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using LeanCloud.Realtime;
using LeanCloud.Storage;

public class GangListPanel : MonoBehaviour {
    public GameObject contentObject;

    public GangItem gangItemPrefab;

    public InputField gangNameInputField;

    async void Start() {
        LCIMConversationQuery query = new LCIMConversationQuery(Realtime.Client);
        query.WhereEqualTo("gang", true);
        ReadOnlyCollection<LCIMConversation> gangConversations = await query.Find();
        foreach (LCIMConversation gangConv in gangConversations) {
            GangItem gangItem = Instantiate(gangItemPrefab);
            gangItem.nameText.text = gangConv.Name;
            gangItem.OnJoin = async () => {
                try {
                    await gangConv.Join();
                    SendMessageUpwards("OnCreateOrJoinGang", gangConv);
                } catch (LCException e) {
                    Debug.LogError($"{e.Code} - {e.Message}");
                }
            };
            gangItem.transform.SetParent(contentObject.transform);
        }
    }

    public async void OnCreateClicked() {
        string name = gangNameInputField.text;
        if (string.IsNullOrEmpty(name)) {
            return;
        }

        try {
            LCIMConversation conversation = await Realtime.Client.CreateConversation(new string[] { },
            name: name,
            unique: false,
            properties: new Dictionary<string, object> {
                { "gang", true }
            });
            Debug.Log($"conversation: {conversation.Id}");
            SendMessageUpwards("OnCreateOrJoinGang", conversation);
        } catch (LCException e) {
            Debug.LogError($"{e.Code} - {e.Message}");
        }
    }    
}
