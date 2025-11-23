using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;
public class SkillDisplay : MonoBehaviour
{
    [SerializeField] List<TMP_Text> skillTexts;
    [SerializeField] List<Button> skillButtons;
    [SerializeField] PlayerStatus player;

    [SerializeField]SelectSkill select;

    public void Awake()
    {
        //select = GetComponent<SelectSkill>();
    }


    public void SetSkills(List<SkillData> skills, int index)
    {
        for (int i = 0; i < skillButtons.Count; i++)
        {
            Debug.Log("kakaka");
            int localIdx = i; // クロージャ対策
            skillButtons[i].onClick.RemoveAllListeners();
            skillButtons[i].onClick.AddListener(() =>select.OnSkillButtonClicked(localIdx));
        }

        for (int i = 0; i < skillTexts.Count; i++)

        {
            int idx = (index + i + skills.Count) - 1 % skills.Count;

            if (skills.Count > 0)

            {

                skillTexts[i].text = " " + skills[idx].skillName + " Cost:" + skills[idx].skillCost.ToString() + " " + skills[idx].skillDescription;
                skillTexts[i].transform.parent.gameObject.SetActive(true);




            }
            else//スキルが2個以下の場合。
            {
                skillTexts[i].text = "";

                skillTexts[i].transform.parent.gameObject.SetActive(false);

            }
        }

        
    }
   
}
