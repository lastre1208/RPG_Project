using UnityEngine;
 public enum SkillElement
    {
        Fire,
        Freeze,
        Spark,
        Wind,
        None
    }

[CreateAssetMenu(menuName ="Skills/AttackSkill")]
public class AttackSkill : SkillData//UŒ‚ƒXƒLƒ‹
{
    public SkillData SkillData;
    public SkillElement Element;
    public float attackRatio;
    
}
