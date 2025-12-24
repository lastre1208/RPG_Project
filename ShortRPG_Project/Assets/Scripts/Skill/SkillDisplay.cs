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
    [SerializeField]TMP_Text skillDiscriptionText;
    [SerializeField]TMP_Text skillCountText;
    [SerializeField] PlayerStatus player;
    [SerializeField]PlayerSPDisplay playerSPDisplay;
    [SerializeField]SelectSkill select;
    [SerializeField] Image shotImage;
    [SerializeField] float blinkSpeed;
   
    Color defaultShotImageColor;
    float alpha = 1.0f;
    
  
    public void Start()
    {
        defaultShotImageColor = shotImage.color;
    }

    public void Update()
    {
        if (player.skillCount == 0)
        {
            alpha = Mathf.PingPong(Time.time * blinkSpeed, 1);
            shotImage.color=new Color(shotImage.color.r,shotImage.color.g,shotImage.color.b,alpha);
        }
        else
        {
            shotImage.color = defaultShotImageColor;
        }
    }
    public void ShowDiscription(int index)
    {
        if (player.status.skillData.Count>=index+1)
        skillDiscriptionText.text = player.status.skillData[index].skillDescription;
    
    }

    public void RemoveDiscription()
    {
        if (player.skillCount > 0)
        {
            skillDiscriptionText.text = "スキルを選択してください";
        }
        else
        {
            skillDiscriptionText.text = "SHOT!!!";
        }
    }

    public void SetSkills(List<SkillData> skills)
    {
       

        skillCountText.text="残り"+player.skillCount.ToString()+"回";
        var count= skills.Count;
        for (int i = 0; i < skillTexts.Count; i++)

        {


            if (i <= count - 1)

            {
                SkillData skill = skills[i];
                int dynamicCost = select.SkillExecuter.SkillUseCount.GetValueOrDefault(skill,0);
                
                int useCost =skill.skillCost+dynamicCost*skill.increaseCost;
                if (dynamicCost != 0)
                {
                    skillTexts[i].text = $"<color=black> {skill.skillName}\n消費SP:<color=red>{useCost}</color></color>";

                   // skillTexts[i].text = " " + skill.skillName + "\n 消費SP:" + useCost.ToString();
                }
                else
                {
                    skillTexts[i].text = " " + skill.skillName + "\n 消費SP:" + useCost.ToString();
                }

                    //  skillTexts[i].transform.parent.gameObject.SetActive(true);


                    var localIndex = i;//クロージャ対策
                skillButtons[i].onClick.RemoveAllListeners();
                skillButtons[i].onClick.AddListener(() => select.OnSkillButtonClicked(localIndex));
                skillButtons[i].onClick.AddListener(() => playerSPDisplay.DelayDamage());
                EnableCheck(skills[i], skillButtons[i],useCost);


            }
            else//スキルが2個以下の場合。
            {
                skillTexts[i].text = "None";
                EnableCheck(null, skillButtons[i],0);


            }
        }

        
    }
   public void EnableCheck(SkillData skill,Button button,int cost)//スキルが使用可能かどうかのチェック
    {
       if(skill==null||cost>player.status.currentSP||player.skillCount<=0)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }


    }
  
}
