using UnityEngine;

public class EnemySelectSkillAndTarget : MonoBehaviour
{

  

    int actionNum=0;

   
    public SkillData SelectSkill(EnemyStatus status)


    {
        switch (status.intelligence)
        {
            case
                EnemyStatus.Intelligence.Fool://ランダムに選出
                {
                    int random = UnityEngine.Random.Range(0, status.status.skillData.Count);

                    return status.status.skillData[random];

                }
            case EnemyStatus.Intelligence.Normal://順番通り
                {
                    actionNum++;
                    return status.status.skillData[actionNum % status.status.skillData.Count];


                }
            case EnemyStatus.Intelligence.Smart://プレイヤーが嫌がるものを選出
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
