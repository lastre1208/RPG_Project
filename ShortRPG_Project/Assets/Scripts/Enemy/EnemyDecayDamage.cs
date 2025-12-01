using Unity.VisualScripting;
using UnityEngine;

public class EnemyDecayDamage : MonoBehaviour
{

   
   public void DacayDamage(Enemy enemy,int damage)//ƒ_ƒ[ƒW”{—¦‚ğŒ¸Š‚³‚¹‚é
    {
        float decayRate = (damage / enemy.easyDecay);

        Debug.Log(decayRate);
        enemy.commonStatus.damageRatio -= decayRate;


            if (enemy.commonStatus.damageRatio < enemy.dacayDamageLimit)
        {
            enemy.commonStatus.damageRatio =enemy.dacayDamageLimit;
        }
    }
   
    public void RecoverDecay(Enemy enemy)
    {

        enemy.commonStatus.damageRatio += enemy.recoverDamageRate;

        if (enemy.commonStatus.damageRatio > 1) { 
        
        enemy.commonStatus.damageRatio = 1f;
        
        }
    }
}
