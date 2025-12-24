using System.Collections.Generic;
using UnityEngine;


public class SkillExecuter : MonoBehaviour
{
  
    Dictionary<SkillData, int> skillUseCount = new();
    public Dictionary<SkillData, int> SkillUseCount
    {
        get { return skillUseCount; }
        set { skillUseCount = value; }
    }
    public bool ExecuteSkill(SkillData skill,CommonStatus user ,CommonStatus target)
    {
        ICommand command = null;

        if (skill == null) return false;
        switch (skill.skillType)
        {
            case SkillType.Attack:
                {
                    command = new AttackCommand();

                    break;
                }
            case SkillType.Heal:
                {
                    command = new HealCommand();
                    break;
                }
            case SkillType.Buff:
                {
                    command = new BuffCommand();
                    break;
                }
            case SkillType.Debuff:
                {
                    command = new DebuffCommand();
                    break;
                }
            case SkillType.Defence:
                {
                    command=new DefendCommand();
                    break;
                }

               
        }
        if (command != null)
        {
            var context = new ActionContext
            { user = user,
                target = target,
                skill = skill,

            };

            // return command.Execute(context);
            if (command.Execute(context))
            {

                user.currentSP -= skill.skillCost+(SkillUseCount.GetValueOrDefault(skill,0)*skill.increaseCost);
                return true;

            }

        }
        return false;

    }
    public void OnSkillUsed(SkillData skill)
    {
        skillUseCount[skill] = skillUseCount.GetValueOrDefault(skill, 0) + 1;


    }
    public void ResetSkillCounts()
    {
        skillUseCount.Clear();
    }
}
