using UnityEngine;

  public enum SkillType
    {
        Attack,
        Heal,
        Buff,
        Debuff,
        Defence
    }
  
public class SkillData : ScriptableObject
{
    [Header("スキル名")]
    public string skillName;
    [Header("説明")]
    public string skillDescription;
    [Header("消費SP")]
    public int skillCost;
    [Header("発動ごとに増えるコスト数")]
    public int increaseCost;
    [Header("スキルタイプ")]
    public SkillType skillType;
    [Header("全体化")]
    public bool isAllTarget;
    
}
