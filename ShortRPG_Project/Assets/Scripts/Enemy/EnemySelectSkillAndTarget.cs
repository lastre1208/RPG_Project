using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemySelectSkillAndTarget : MonoBehaviour
{

  

    int actionNum=0;

   
    public SkillData SelectSkill(Enemy status)
    {

        if (status.commonStatus.StateCheck(status.commonStatus, EnableState.Sleep))
        {
            Debug.Log("おねんね");
            return null; 
        }
        else if (status.commonStatus.StateCheck(status.commonStatus, EnableState.Panic))//馬鹿にする
        {
            status.Intelligence = Intelligence.Fool;
        }
        
            switch (status.Intelligence)
            {
                case
                    Intelligence.Fool://ランダムに選出
                    {
                        int random = UnityEngine.Random.Range(0, status.commonStatus.skillData.Count);

                        return status.commonStatus.skillData[random];

                    }
                case Intelligence.Normal://順番通り
                    {
                        actionNum++;
                        return status.commonStatus.skillData[actionNum % status.commonStatus.skillData.Count];


                    }
                case Intelligence.Smart://プレイヤーが嫌がるものを選出
                    {
                        break;
                    }
            }

        return null;
    }
    public CommonStatus SelectTarget(SkillData skill,CommonStatus player,CommonStatus user)//スキルごとにターゲットが変わる
    {
        CommonStatus target;

        switch (skill.skillType)
        {
            case SkillType.Attack:
                {

                    target = player;
                    return target;
                }
            case SkillType.Heal:
                {

                    target = user;
                    return target;
                }
            case SkillType.Defence:
                {

                    target = user;
                    return target;
                }
            case SkillType.Buff:
                {
                    target = user;
                    return target;
                }
            case SkillType.Debuff:
                {
                    target = player;
                      
                    return target;
                }

        }




        return null;
    }

  

}
