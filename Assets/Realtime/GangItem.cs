using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GangItem : MonoBehaviour {
    public Text nameText;

    public Action OnJoin {
        get; set;
    }

    public void OnJoinClicked() {
        OnJoin?.Invoke();
    }
}
