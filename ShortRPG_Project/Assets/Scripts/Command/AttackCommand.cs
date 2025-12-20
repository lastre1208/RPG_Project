using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
public class AttackCommand : ICommand//攻撃系。今は通常攻撃のみ。
{
   public bool Execute( ActionContext action )
   {
      AttackSkill skill=action.skill as AttackSkill;




       action.target.TakeDamage((int)(action.user.attackPower*skill.attackRatio*action.user.damageRatio-action.target.defensePower));
        
        Debug.Log(action.user.characterName + "の攻撃！" + action.target.characterName + "に" + (int)(action.user.attackPower - action.target.defensePower) + "のダメージ！");
        if (action.target.IsDead())
        {
            Debug.Log(action.target.characterName + "は倒れた！");
        }

        return true;
    }

}
