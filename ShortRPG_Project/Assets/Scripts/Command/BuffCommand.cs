using UnityEngine;
public class BuffCommand : ICommand
{
    public bool Execute(ActionContext action)
    {
        var buffSkill = action.skill as BuffSkill;
        if (buffSkill == null)
        {
            Debug.LogWarning("スキルの型が不正");
            return false;
        }

        // バフ対象（自身 or 味方など）を取得
        var user = action.user;
        var target = action.target; 
        if (target == null)
        {
            Debug.LogWarning("バフ対象が未設定です");
            return false;
        }

        if (user.currentSP < buffSkill.skillCost)
        {
            Debug.Log("SP不足！！");
            return false;
        }
        // 例: ステータス値にバフを加算（安全のためswitch文で分岐）


        foreach (var skill in buffSkill.Effect)
        {

            switch (skill.status)
            {
                case ModifyStatus.Power:
                    target.AddBuff(ModifyStatus.Power, skill.level, buffSkill.enableTurn);
                    break;
                case ModifyStatus.Defence:
                    target.AddBuff(ModifyStatus.Defence, skill.level, buffSkill.enableTurn);
                    break;
                case ModifyStatus.Interval:
                    target.AddBuff(ModifyStatus.Interval, skill.level, buffSkill.enableTurn);
                    break;
                case ModifyStatus.Scale:
                    target.AddBuff(ModifyStatus.Scale, skill.level, buffSkill.enableTurn);
                    break;
                case ModifyStatus.Time:
                    target.AddBuff(ModifyStatus.Time, skill.level, buffSkill.enableTurn);
                    break;

                    // 他のステータスも同様に分岐
            }


        }
      
        user.currentSP -= buffSkill .skillCost;
      //  Debug.Log($"[BuffCommand] {target}に{buffSkill.status}バフ（{buffSkill.modifiRatio}、{buffSkill.enableTurn}ターン）付与");

        return true;
    }
}
