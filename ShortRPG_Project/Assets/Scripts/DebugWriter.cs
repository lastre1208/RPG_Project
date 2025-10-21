using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DebugWriter : MonoBehaviour
{
    public TMP_Text logText; // InspectorにText UIをセット
    string log = "";

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }
    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }
    void HandleLog(string message, string stackTrace, LogType type)
    {
        log += message + "\n";
        if (log.Length > 1000) // 適度に古いログをカット
            log = log.Substring(log.Length - 1000);
        logText.text = log;
    }
}
