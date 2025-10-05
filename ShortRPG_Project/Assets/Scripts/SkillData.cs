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
    public string skillName;    
    public int skillCost;
    public SkillType skillType;
    public bool isAllTarget;
    
}
