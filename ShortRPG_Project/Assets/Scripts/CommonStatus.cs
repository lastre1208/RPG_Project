using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
[System.Serializable]
public class CommonStatus //共通処理＆ステータス。
{
    public Sprite characterImage;
 public string characterName;
    public int maxHP;
    public int currentHP;
    public int maxMP;
    public int currentMP;
    public int attackPower;
    public int defensePower;
    public float damageRatio = 1.0f;
    public List<SkillData> skillData;//持っているスキルリスト
   
   
   
}
