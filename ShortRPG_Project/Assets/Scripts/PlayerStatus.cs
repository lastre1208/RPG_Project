using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using System;

[System.Serializable]


public class DefaultPlayerStatus
{
    public CommonStatus CommonStatus;
    public float Scale;
    public float Interval;
    public float Time;
}

[System.Serializable]
public class PlayerStatus :MonoBehaviour, ICharacterSet,IBuffEffect,IDebuffEffect
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
    public int hitCount=0;
    public int maxHitCount=0;
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
        Default.CommonStatus = new();
        Default.CommonStatus.attackPower = common.attackPower;
        Default.CommonStatus.defensePower=common.defensePower;
        Default.CommonStatus.damageRatio=common.damageRatio;

    }
    public void Awake()
    {
        status.Inject(this, this,this);

        status.currentHP = status.maxHP;
        status.currentSP = status.maxSP;

       

    }
    
    public void ReturnDefault(PlayerStatus player)//バフ/デバフ前に呼んでステータスを初期化
    {
        player.status.attackPower = Default.CommonStatus.attackPower;
        player.status.defensePower=Default.CommonStatus.defensePower;
        player.status.damageRatio = Default.CommonStatus.damageRatio;
        player.shotTime = Default.Time;
        player.bulletScale = Default.Scale;
        player.intervalRatio = Default.Interval;
       

    }
    // バフによる補正後のステータスを再計算
    public void ApplyBuffEffect(List<BuffEntry>buffs)
    {

        ReturnDefault(this);
        foreach (var buff in buffs)
        {
            switch (buff.status)
            {
                case ModifyStatus.Power:
                    status.attackPower *= buff.ratio;
                    status.attackPower = Mathf.Round(status.attackPower);
                    break;
                case ModifyStatus.Defence:
                    status.defensePower *= buff.ratio;
                    status.defensePower = Mathf.Round(status.defensePower);
                    break;
                case ModifyStatus.Interval:
                    intervalRatio *= buff.ratio;
                    intervalRatio = (float)Math.Round(intervalRatio, 2);
                    break;
                case ModifyStatus.Scale:

                    bulletScale *= buff.ratio;
                    bulletScale = (float)Math.Round(bulletScale, 2);
                    break;
                    // 必要に応じて追加
            }
        }
    }
    public void ApplyDebuffEffect(List<DebuffEntry> debuffs)
    {

        foreach (var debuff in debuffs)
        {

            switch (debuff.enableState)
            {

                case EnableState.Sleep://インターバルが長くなる
                    {
                        //intervalRatio *= 1.5f;
                        break;
                    }
                case EnableState.Palysis://発射可能時間が減る
                    {
                       // shotTime *= 0.5f;
                        break;
                    }
                case EnableState.Panic://発射間隔がバラバラになる
                    {
                        break;
                    }
                case EnableState.Mind://受けるダメージが倍になる
                    {

                      //  status.damageRatio *= 2;
                        break;
                    }
            }

        }

    }
   

}
