using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Runtime.InteropServices;


[System.Serializable]


public class DefaultPlayerStatus
{
    
    public float Scale;
    public float Interval;
    public float Time;
}

[System.Serializable]
public class PlayerStatus :MonoBehaviour, ICharacterSet,IBuffEffect
{
    public CommonStatus status;
   
    public float shotTime;
    public float recoverSp;
    public float intervalRatio=1f;
    public float bulletScale;//弾の大きさ倍率
    public int exp;
    public int nextExp;
    public int level=1; 
    public Weapon equippedWeapon;
    public List<Weapon> havedWeapon;
   
    private DefaultPlayerStatus Default=new ();
   


    public void SetDefault(CommonStatus common)
    {
        common.attackPower = status.attackPower;
        common.defensePower = status.defensePower;
        common.maxHP = status.maxHP;
        common.maxSP = status.maxSP;
        common.damageRatio = status.damageRatio;

        Default.Scale = bulletScale;
        Default.Interval = intervalRatio;
        Default.Time = shotTime;


    }
    public void Awake()
    {
        status.Inject(this, this);

        status.currentHP = status.maxHP;
        status.currentSP = status.maxSP;

    }
    
    // バフによる補正後のステータスを再計算
    public void ApplyBuffEffect(List<BuffEntry>buffs)
    {
     
        foreach (var buff in buffs)
        {
            switch (buff.status)
            {
                case ModifyStatus.Power:
                    status.attackPower*= buff.ratio;
                    break;
                case ModifyStatus.Defence:
                    status.defensePower*=  buff.ratio;
                    break;
                case ModifyStatus.Interval:
                    intervalRatio *= buff.ratio;
                    break;
                case ModifyStatus.Scale:
                    bulletScale *= buff.ratio;
                    break;
                    // 必要に応じて追加
            }
        }
    }

   

}
