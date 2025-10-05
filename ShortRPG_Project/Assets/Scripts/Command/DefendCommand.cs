using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
public class DefendCommand : ICommand
{
    
    public void  Execute(ActionContext action)
    {
        DefendSkill skill = action.skill as DefendSkill;
        Debug.Log(action.user.characterName + "�͖h�䂵���I");
        action.user.isDefending = true;
        action.user.damageRatio=skill.cutRatio;
    }
}
