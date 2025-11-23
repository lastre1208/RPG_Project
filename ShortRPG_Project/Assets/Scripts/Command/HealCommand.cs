using Unity.VisualScripting;
using UnityEngine;

public class HealCommand : ICommand
{
  public void Execute(ActionContext action)//Š„‡‰ñ•œ
    {
        var healSkill = action.skill as HealSkill;

        if (action.target.maxHP == action.target.currentHP)   return;

        action.target.currentHP += (int)((action.target.maxHP / healSkill.healPower));
        if(action.target.currentHP>action.target.maxHP)
        {
            action.target.currentHP = action.target.maxHP;
        }
        action.user.currentSP -= healSkill.skillCost;
    }
}
