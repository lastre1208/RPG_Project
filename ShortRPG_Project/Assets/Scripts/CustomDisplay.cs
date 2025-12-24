using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CustomManager;
public class CustomDisplay : MonoBehaviour
{
  [SerializeField]CustomManager manager;
    [SerializeField] TMP_Text discriptionText;
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text spText;
    [SerializeField] TMP_Text attackText;
    [SerializeField] TMP_Text defenceText;
    [SerializeField] List<Button> increaseButtons;
    [SerializeField]List<Button> decreaseButtons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void CheckEnableCustom()
    {

        foreach (var button in decreaseButtons)
        {

            StatusButtonManager skillButton = button.GetComponent<StatusButtonManager>();

            CustomType type = skillButton.Type;


            bool hasPoints = manager.customPoints[type] > 0;
            button.interactable = hasPoints;

        }
        foreach (var button in increaseButtons)
        {

            StatusButtonManager skillButton = button.GetComponent<StatusButtonManager>();

            CustomType type = skillButton.Type;


            bool hasPoints = manager.player.skillPoints > 0;
            button.interactable = hasPoints;

        }
    }
    // Update is called once per frame
    void Update()
    { 

        discriptionText.text = "残りポイント:" + manager.player.skillPoints;
        hpText.text = "HP:" + manager.player.status.maxHP + "+" + manager.customPoints[CustomManager.CustomType.HP];
        spText.text="SP:"+manager.player.status.maxSP+"+"+manager.customPoints[CustomManager.CustomType.SP];
        attackText.text = "攻撃力:" + manager.player.status.attackPower + "+" + manager.customPoints[CustomManager.CustomType.Attack];
        defenceText.text = "防御力:" + manager.player.status.defensePower + "+" + manager.customPoints[CustomManager.CustomType.Defence];

        CheckEnableCustom();
    }
}
