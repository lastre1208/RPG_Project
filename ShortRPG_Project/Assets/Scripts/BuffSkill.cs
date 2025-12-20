using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class BuffEffect
{
    public ModifyStatus status;//どのステータスを変化させるか
    public BuffLevel level;
}

public enum BuffLevel
{
    WeakMin=-4,
    Weak3=-3, 
    Weak2=-2,
    Weak1=-1,
    None=0,
    Buff1 = 1,
    Buff2 = 2,
    Buff3 = 3,
    Buff4 = 4,
    Buff5=5,
    BuffMax= 6,

}
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


[CreateAssetMenu(menuName = "Skills/BuffSkill")]
public class BuffSkill : SkillData//状態変化系スキル
{
    public SkillData SkillData;
    public List< BuffEffect> Effect;
   
    public int enableTurn;//効果持続ターン数


}
