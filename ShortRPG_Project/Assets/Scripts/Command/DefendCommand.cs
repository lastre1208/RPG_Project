using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
public class DefendCommand : ICommand
{
    
    public bool  Execute(ActionContext action)
    {
        DefendSkill skill = action.skill as DefendSkill;
      //  Debug.Log(action.user.characterName + "ÇÕñhå‰ÇµÇΩÅI");
       // action.user.isDefending = true;
        action.user.damageRatio=skill.cutRatio;

        return true;
    }
}
