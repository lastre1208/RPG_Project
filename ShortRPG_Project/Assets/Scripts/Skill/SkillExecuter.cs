using UnityEngine;


public class SkillExecuter : MonoBehaviour
{
   
   

    public void ExecuteSkill(SkillData skill,CommonStatus user ,CommonStatus target)
    {
        ICommand command = null;

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

            command.Execute(context);

        }


    }

}
