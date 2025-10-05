using UnityEngine;
public enum ModifyStatus
{
    Power,
    Defence,
    Speed,
    None,
}

public class StateChangeSkill : SkillData//状態変化系スキル
{
    public SkillData SkillData;
    public ModifyStatus status;//どのステータスを変化させるか
    public float modifiRatio;//変化倍率
    public int enableTurn;
    
}
