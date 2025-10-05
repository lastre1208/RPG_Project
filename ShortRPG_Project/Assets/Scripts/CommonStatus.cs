using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class CommonStatus //���ʏ������X�e�[�^�X�B
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
    public List<SkillData> skillData;//�����Ă���X�L�����X�g

    public void TakeDamage(int damage)
    {
        if (isDefending)//�h�䂵�Ă�Δ���
        {
            damage /= (int)damageRatio;
        }
        currentHP-=damage;
        Debug.Log(characterName + "��" + damage + "�_���[�W���󂯂��I");
    }
    public bool IsDead()
    {

        return currentHP <= 0;  
    }
}
