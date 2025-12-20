using JetBrains.Annotations;
using System.Collections.Generic;
using System;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RecoverData
{
    public int ratio;
    public Image main;
}
public class DamageData
{
    public Vector2 hitPosition;
    public int damage;
    public Image damageHP;
   
}
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
    public BuffLevel level;
    public int RemainTurn {
        get;set;
    }

    public BuffEntry(ModifyStatus status, BuffLevel level, int remainTurn)
    {
        this.status = status;
        this.level = level;
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
    public int attackPower;
    public int defensePower;
    public float damageRatio = 1.0f;
    public List<SkillData> skillData;//持っているスキルリスト
    public List<BuffEntry> buffs = new List<BuffEntry>();
    public  List<DebuffEntry> debuffs;
    ICharacterSet characterSet;
    IBuffEffect buffEffect;
    IDebuffEffect debuffEffect;
  public  DamageData damageData=new DamageData();
    public RecoverData recoverData = new RecoverData();
   public  event Action<DamageData> OnDamage;
    public event Action OnRecover;
    public event Action OnSPRecover;
    const int PARCENT_CONVERSION = 100;
    const float PARCENT_BUFFLEVEL = 0.25f;
    public CommonStatus() { }


    public void Inject(ICharacterSet set, IBuffEffect effect,IDebuffEffect d_effect)
    {
        characterSet = set;
        buffEffect = effect;
        debuffEffect = d_effect;
        buffs.Clear();
        debuffs.Clear();
        // ここで初期化
        characterSet?.SetDefault(this);
    }
    public void AddBuff(ModifyStatus modifyStatus, BuffLevel level, int turn)
    {
       var existing=buffs.FirstOrDefault(x => x.status == modifyStatus);
            if (existing!=null&&existing.RemainTurn>0)
            {

                existing.level += (int)level;
            
                existing.RemainTurn = turn;

            }
            else
            {
                buffs.Add(new BuffEntry(modifyStatus, level, turn));
            }


            buffEffect?.ApplyBuffEffect(buffs);
        
    }
    public void AddDebuff( EnableState state, int turn)
    {
        debuffs.Add(new DebuffEntry(state,turn));
        debuffEffect?.ApplyDebuffEffect(debuffs);
    }

    // 各ターン終了時などで呼ぶ：残りターン減少＋無効バフ消去
    public void UpdateBuffsPerTurn()
    {

        RemainCount(buffs);
        RemainCount(debuffs);
        buffEffect?.ApplyBuffEffect(buffs);
        debuffEffect?.ApplyDebuffEffect(debuffs);

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
    public void RemainDebuffCount(List<DebuffEntry> entries) 
    {
        foreach (DebuffEntry entry in entries)
        {
            entry.RemainTurn--;

            if (entry.RemainTurn <= 0)
            {
                entries.Remove(entry);
            }
        }

        
    }

    public bool StateCheck(CommonStatus status,EnableState state)//特定の状態異常にかかっているかどうかのチェック
    {
      foreach(var entry in status.debuffs)
        {
            if(entry!= null && entry.enableState == state)
            {
                return true;
            }

        }
        
        return false;
    }
    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            damage = 0;
        }
        else
        {
            currentHP -= damage;
            damageData.damage = damage;
            OnDamage?.Invoke(damageData);
        }
           
        

    }
   
    public int CalculationDamage(int attack,int defence)
    {


        return attack - defence;
    }
    public void TakeHeal(int heal)
    {

        currentHP += (maxHP * heal)/PARCENT_CONVERSION;
        if(currentHP >maxHP)
        {
            currentHP = maxHP;
        }
        OnRecover?.Invoke();
    }
    public void SPHeal(int heal)
    {

        currentSP += (maxSP * heal) / PARCENT_CONVERSION;
        if (currentSP > maxSP)
        {
            currentSP = maxSP;
        }
        OnSPRecover?.Invoke();
    }

    public bool IsDead()
    {

        return currentHP <= 0;
    }
    public  float GetMultiplier(BuffLevel level,bool isReverse)
    {
        var multi=0f;
        if (!isReverse)
        {
            multi = 1 + ((float)level * PARCENT_BUFFLEVEL);
        }
        else
        {
            multi = 1 - ((float)level * PARCENT_BUFFLEVEL);
        }


            return multi;
    }
}
