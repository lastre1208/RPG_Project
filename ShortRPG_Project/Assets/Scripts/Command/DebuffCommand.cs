using UnityEngine;

public class DebuffCommand :ICommand
{
    public bool Execute(ActionContext action)
    {
        var debuffSkill=action.skill as DebuffSkill;

        if (debuffSkill == null) return false;

        var user = action.user;

        var target= action.target;


        if (user.currentSP < debuffSkill.skillCost)
        {
            Debug.Log("Sp不足！！！");
            return false;
        }
        var random = Random.Range(0, 100);
        var isHit = debuffSkill.hitRate >= random;

        if (isHit)//命中したら状態異常を付与
        {
            switch (debuffSkill.state)
            {
                case EnableState.Sleep:
                    {
                        target.AddDebuff(EnableState.Sleep, debuffSkill.enableTurn);
                        break;
                    }
                case EnableState.Palysis:
                    {
                        target.AddDebuff(EnableState.Palysis,debuffSkill.enableTurn);
                        break;
                    }
                case EnableState.Panic:
                    {
                        target.AddDebuff(EnableState.Panic, debuffSkill.enableTurn);
                        break;
                    }
                case EnableState.Mind:
                    {
                        target.AddDebuff(EnableState.Mind, debuffSkill.enableTurn);
                        break;
                    }


            }
        }
        else
        {
            Debug.Log("ミス！");
        }
      //  user.currentSP -= debuffSkill.skillCost;

        return true;

    }
}
