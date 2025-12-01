using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class SelectSkill : MonoBehaviour
{
    [SerializeField]BattleManager battleManager;
    [SerializeField] SkillExecuter skillExecuter;
    [SerializeField] SkillDisplay skillDisplay;
    [SerializeField] GameObject selectUI;
    PlayerStatus player;
    int currentIndex = 0;
    List<SkillData> skills;
    public void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerStatus>(); 
        selectUI.SetActive(false);
    }

    //public void OnSelectSkillButton()
    //{

    //}
    public void OnSkillButtonClicked(int btnIdx)
    {
        int selectedSkillIndex = (currentIndex + btnIdx - 1 + skills.Count) % skills.Count;
        var selectedSkill = skills[selectedSkillIndex];

        if (selectedSkill.isAllTarget&&selectedSkill.skillType==SkillType.Debuff)//ìGÇ…ëŒÇµÇƒÇÃèÛë‘àŸèÌïtó^ÇÕÇ±ÇøÇÁ
        {
            foreach(var enemy in battleManager.enemies)
            {
                skillExecuter.ExecuteSkill(selectedSkill, player.status, enemy.commonStatus);
            }
        }
        else
        {
            skillExecuter.ExecuteSkill(selectedSkill, player.status, player.status);

        }
            
    }
    public void StartSelect()
    {
        selectUI.SetActive(true);
        currentIndex = 0;
        skills = player.status.skillData;
        skillDisplay.SetSkills(player.status.skillData, currentIndex);


    }
    public void MoveUp()
    {
        if (skills.Count == 0 || skills == null) return;
        currentIndex = (currentIndex - 1 + skills.Count) % skills.Count;
        skillDisplay.SetSkills(player.status.skillData, currentIndex);

    }

    public void MoveDown()
    {
        if (skills.Count == 0 || skills == null) return;
        currentIndex = (currentIndex + 1) % skills.Count;
        skillDisplay.SetSkills(player.status.skillData, currentIndex);

    }
}
