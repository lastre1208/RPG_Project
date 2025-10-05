using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;
using TMPro;
public class SelectTarget : MonoBehaviour
{
    public GameObject targetPanel; //�^�[�Q�b�g�I���p�l��
    public TMP_Text targetText; //�^�[�Q�b�g�I���e�L�X�g
    public CommandSelect command;
    public CanvasGroup commandUI;
    EnemyStatus targetEnemy;
    int targetIndex;
    private void Start()
    {
        targetText = targetPanel.GetComponentInChildren<TMP_Text>();
        targetPanel.SetActive(false); //�ŏ��͔�\��
    }
    public void Update()
    {
        if (targetPanel.activeSelf)
        {

            targetText.text = targetEnemy.status.characterName;
        }
    }
    public void TargetStart()//�^�[�Q�b�g�I���J�n
    {
        targetPanel.SetActive(true);
        targetEnemy = command.manager.enemies.First();
        commandUI.interactable = false;
        targetIndex = 0;
    }

    public void TargetNext()//�^�[�Q�b�g�̐؂�ւ�
    {
        if (!targetPanel.activeSelf) return;

        targetIndex=(targetIndex+1)%command.manager.enemies.Count;
        targetEnemy=command.manager.enemies[targetIndex];
       
    }
    public void TargetPrevious()//�^�[�Q�b�g�̐؂�ւ�
    {
        if (!targetPanel.activeSelf) return;

        targetIndex = (targetIndex - 1+command.manager.enemies.Count) % command.manager.enemies.Count;
        targetEnemy = command.manager.enemies[targetIndex];
    }

    public void Canceltarget(InputAction.CallbackContext context)//�^�[�Q�b�g�I���L�����Z��
    {
        if (!targetPanel.activeSelf) return;
        if (context.started)
        {
            EndSelect();
            Debug.Log("�L�����Z���I");
        }

    }
    public void OnTargetButton()//�^�[�Q�b�g�I���{�^���������ꂽ�Ƃ�
    {
        EndSelect();
        Debug.Log("�^�[�Q�b�g�I������");
        command.Target=targetEnemy;
        command.PlayAction();
    }

    public void EndSelect()
    {
        targetPanel.SetActive(false);
        commandUI.interactable = false;
    }
}
