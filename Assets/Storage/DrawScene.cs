using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using LeanCloud.Storage;

public class DrawScene : MonoBehaviour {
    public Text resultText;

    public async void OnDrawClicked() {
        try {
            Dictionary<string, object> results = await LCCloud.Run("draw");
            List<object> heros = results["result"] as List<object>;
            string info = $"恭喜获得：{string.Join(", ", heros)}";
            resultText.text = info;
            Debug.Log(info);
        } catch (Exception e) {
            Debug.LogError(e);
        }    
    }
}
