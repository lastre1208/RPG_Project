using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DebugWriter : MonoBehaviour
{
    public TMP_Text logText; // Inspector��Text UI���Z�b�g
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
        if (log.Length > 1000) // �K�x�ɌÂ����O���J�b�g
            log = log.Substring(log.Length - 1000);
        logText.text = log;
    }
}
