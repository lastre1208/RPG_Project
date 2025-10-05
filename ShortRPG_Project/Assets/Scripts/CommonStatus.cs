using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class CommonStatus //共通処理＆ステータス。
{
    
 public string characterName;
    public int maxHP;
    public int currentHP;
    public int maxMP;
    public int currentMP;
    public int attackPower;
    public int defensePower;
    public int speed;
    public bool isDefending = false;
    public float damageRatio = 1.0f;
    public List<SkillData> skillData;//持っているスキルリスト

    public void TakeDamage(int damage)
    {
        if (isDefending)//防御してれば半減
        {
            damage /= (int)damageRatio;
        }
        currentHP-=damage;
        Debug.Log(characterName + "は" + damage + "ダメージを受けた！");
    }
    public bool IsDead()
    {

        return currentHP <= 0;  
    }
}
