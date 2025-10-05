using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;
using TMPro;
public class SelectTarget : MonoBehaviour
{
    public GameObject targetPanel; //ターゲット選択パネル
    public TMP_Text targetText; //ターゲット選択テキスト
    public CommandSelect command;
    public CanvasGroup commandUI;
    EnemyStatus targetEnemy;
    int targetIndex;
    private void Start()
    {
        targetText = targetPanel.GetComponentInChildren<TMP_Text>();
        targetPanel.SetActive(false); //最初は非表示
    }
    public void Update()
    {
        if (targetPanel.activeSelf)
        {

            targetText.text = targetEnemy.status.characterName;
        }
    }
    public void TargetStart()//ターゲット選択開始
    {
        targetPanel.SetActive(true);
        targetEnemy = command.manager.enemies.First();
        commandUI.interactable = false;
        targetIndex = 0;
    }

    public void TargetNext()//ターゲットの切り替え
    {
        if (!targetPanel.activeSelf) return;

        targetIndex=(targetIndex+1)%command.manager.enemies.Count;
        targetEnemy=command.manager.enemies[targetIndex];
       
    }
    public void TargetPrevious()//ターゲットの切り替え
    {
        if (!targetPanel.activeSelf) return;

        targetIndex = (targetIndex - 1+command.manager.enemies.Count) % command.manager.enemies.Count;
        targetEnemy = command.manager.enemies[targetIndex];
    }

    public void Canceltarget(InputAction.CallbackContext context)//ターゲット選択キャンセル
    {
        if (!targetPanel.activeSelf) return;
        if (context.started)
        {
            EndSelect();
            Debug.Log("キャンセル！");
        }

    }
    public void OnTargetButton()//ターゲット選択ボタンが押されたとき
    {
        EndSelect();
        Debug.Log("ターゲット選択完了");
        command.Target=targetEnemy;
        command.PlayAction();
    }

    public void EndSelect()
    {
        targetPanel.SetActive(false);
        commandUI.interactable = false;
    }
}
