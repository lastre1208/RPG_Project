using UnityEngine;


public enum EnableState//•t—^‚·‚éó‘ÔˆÙí
{
    None,
    Sleep,
    Mind,
    Panic,
    Palysis,
}
[CreateAssetMenu(menuName = "Skills/DebuffSkill")]
public class DebuffSkill : SkillData
{
  public  SkillData skill;
    public EnableState state;//‚Ç‚ñ‚Èó‘ÔˆÙí‚ğ•t—^‚·‚é‚©
    public int enableTurn;
    public int hitRate;
}
