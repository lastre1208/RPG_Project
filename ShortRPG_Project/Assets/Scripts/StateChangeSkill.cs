using UnityEngine;
public enum ModifyStatus//変化させるステータスの種類
{
    Power,
    Defence,
    Speed,
    Interval,
    Scale,
    Time,
    None,
}

public class BuffSkill : SkillData//状態変化系スキル
{
    public SkillData SkillData;
    public ModifyStatus status;//どのステータスを変化させるか
    public float modifiRatio;//変化倍率
    public int enableTurn;//効果持続ターン数

}
