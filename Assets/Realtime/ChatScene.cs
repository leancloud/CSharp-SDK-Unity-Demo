using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using LeanCloud.Storage;
using LeanCloud.Realtime;

public class ChatScene : MonoBehaviour {
    public Text idText;

    public Toggle worldToggle;
    public Toggle gangToggle;
    public Toggle privateToggle;

    public GameObject worldPanel;
    public GameObject gangPanel;
    public GameObject privatePanel;

    void Start() {
        idText.text = $"ID: {Realtime.Client.Id}";

        worldToggle.onValueChanged.AddListener(selected => TogglePanel());
        gangToggle.onValueChanged.AddListener(selected => TogglePanel());
        privateToggle.onValueChanged.AddListener(selected => TogglePanel());

        TogglePanel();
    }

    private void TogglePanel() {
        worldToggle.GetComponent<Image>().color = worldToggle.isOn ? Color.red : Color.white;
        gangToggle.GetComponent<Image>().color = gangToggle.isOn ? Color.red : Color.white;
        privateToggle.GetComponent<Image>().color = privateToggle.isOn ? Color.red : Color.white;

        worldPanel.SetActive(worldToggle.isOn);
        gangPanel.SetActive(gangToggle.isOn);
        privatePanel.SetActive(privateToggle.isOn);
    }

    void OnDestroy() {
        _ = Realtime.Client.Close();
    }
}
