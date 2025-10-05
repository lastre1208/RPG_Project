using UnityEngine;


public enum EnableState//•t—^‚·‚éó‘ÔˆÙí
{
    None,
    Speep,
    Mind,
    Panic,
    Palysis,
}
[CreateAssetMenu(menuName = "Skills/DebuffSkill")]
public class DebuffSkill : StateChangeSkill
{
    StateChangeSkill skill;
}
