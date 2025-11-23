using JetBrains.Annotations;
using System.Collections.Generic;
using System;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditorInternal.ReorderableList;
using UnityEngine.XR;


public interface ITurnEntry
{
    int RemainTurn
    {
        get;set;
    }
}

[System.Serializable]
public class BuffEntry:ITurnEntry
{
    public ModifyStatus status;
    public float ratio;
    public int RemainTurn {
        get;set;
    }

    public BuffEntry(ModifyStatus status, float ratio, int remainTurn)
    {
        this.status = status;
        this.ratio = ratio;
        this.RemainTurn = remainTurn;
    }
}

[System.Serializable]


public class DebuffEntry:ITurnEntry
{
    
    public EnableState enableState;
  
    public int RemainTurn
    {
        get;set;
    }

    public DebuffEntry( EnableState state, int remainTurn)
    {

        this.enableState = state;
      
        this.RemainTurn = remainTurn;
    }
}


[System.Serializable]
public class CommonStatus //共通処理＆ステータス。
{
    public Sprite characterImage;
    public string characterName;
    public int maxHP;
    public int currentHP;
    public int maxSP;
    public int currentSP;
    public float attackPower;
    public float defensePower;
    public float damageRatio = 1.0f;
    public List<SkillData> skillData;//持っているスキルリスト
    public List<BuffEntry> buffs = new List<BuffEntry>();
    public List<DebuffEntry>debuffs = new List<DebuffEntry>();
    ICharacterSet characterSet;
    IBuffEffect buffEffect;

    public CommonStatus() { }




    public void Inject(ICharacterSet set, IBuffEffect effect)
    {
        characterSet = set;
        buffEffect = effect;

        // ここで初期化
        characterSet?.SetDefault(this);
    }
    public void AddBuff(ModifyStatus modifyStatus, float ratio, int turn)
    {

        buffs.Add(new BuffEntry(modifyStatus, ratio, turn));
        if (buffEffect != null)
        {
            buffEffect.ApplyBuffEffect(buffs);

        }
      
    }
    public void AddDebuff(ModifyStatus modifyStatus, EnableState state,float ratio, int turn)
    {
        debuffs.Add(new DebuffEntry(state,turn));
    }

    // 各ターン終了時などで呼ぶ：残りターン減少＋無効バフ消去
    public void UpdateBuffsPerTurn()
    {
        RemainCount(buffs);
        RemainCount(debuffs);


    }
    public void RemainCount<T>(List<T> entries) where T : ITurnEntry
    {
        for (int i = entries.Count - 1; i >= 0; i--)
        {
            entries[i].RemainTurn--;

            if (entries[i].RemainTurn <= 0)
            {
                entries.RemoveAt(i);
            }


        }
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            damage = 0;
        }
        currentHP -= damage;


    }

    public bool IsDead()
    {

        return currentHP <= 0;
    }
}
