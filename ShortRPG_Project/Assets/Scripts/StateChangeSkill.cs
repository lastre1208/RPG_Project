using UnityEngine;
public enum ModifyStatus
{
    Power,
    Defence,
    Speed,
    None,
}

public class StateChangeSkill : SkillData//��ԕω��n�X�L��
{
    public SkillData SkillData;
    public ModifyStatus status;//�ǂ̃X�e�[�^�X��ω������邩
    public float modifiRatio;//�ω��{��
    public int enableTurn;
    
}
