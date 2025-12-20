using UnityEngine;

public class CustomButtonHandler : MonoBehaviour
{

    [SerializeField] Weapon playerWeapon;
 
  public  CustomData data;
  public void OnClicked()
    {
        foreach(var effect in data.effects)

        switch (effect.Type)
        {
            case Custom.Scale:
            {

                    playerWeapon.bulletScale *= effect.influenceRate;
                    break;

            }
            case Custom.Interval:
                {
                    playerWeapon.fireInterval *= effect.influenceRate;
                    break;
                }
            case Custom.Attack:
                {
                        playerWeapon.attackPower = Mathf.RoundToInt(
               playerWeapon.attackPower * effect.influenceRate
           );
                        //  playerWeapon.attackPower*= (int)effect.influenceRate;
                        break;
                }


        }

        if (data.getSkill != null)
        {
            playerWeapon.player.status.skillData.Add(data.getSkill);
        }
    }
}
