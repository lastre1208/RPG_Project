using UnityEngine;


public enum EnableState//�t�^�����Ԉُ�
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
