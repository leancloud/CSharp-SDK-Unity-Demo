using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using LeanCloud;
using LeanCloud.Storage;
using LeanCloud.Realtime;

public static class Realtime {
    public static LCIMClient Client {
        get; set;
    }

    public static void Init(string clientId) {
        LCApplication.Initialize("TiuIJfLckzsDeBJrssNNRFVi-gzGzoHsz",
            "uyuRgnt0WQWlWNlhhd83lp0f",
            "https://tiuijflc.lc-cn-n1-shared.com");
        LCLogger.LogDelegate = (level, log) => {
            switch (level) {
                case LCLogLevel.Debug:
                    Debug.Log(log);
                    break;
                case LCLogLevel.Warn:
                    Debug.LogWarning(log);
                    break;
                case LCLogLevel.Error:
                    Debug.LogError(log);
                    break;
                default:
                    break;
            }
        };
        Client = new LCIMClient(clientId);
    }
}
