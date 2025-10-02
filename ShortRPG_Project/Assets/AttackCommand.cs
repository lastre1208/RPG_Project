using UnityEngine;
using System.Collections.Generic;
public class AttackCommand : ICommand
{
   public void Execute( CharacterStatus commander,CharacterStatus targets )
   {
      
        targets.TakeDamage(commander.attackPower);
        
       if (targets.IsDead())
        {
            Debug.Log(targets.characterName + "ÇÕì|ÇÍÇΩÅI");
        }
    }

}
