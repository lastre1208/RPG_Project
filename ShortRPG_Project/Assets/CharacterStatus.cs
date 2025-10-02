using UnityEngine;
[System.Serializable]
public class CharacterStatus 
{
  public string characterName;
    public int ID;
    public int maxHP;
    public int currentHP;
    public int attackPower;
    public int defensePower;
    public int speed;

    public CharacterStatus(string name, int id, int hp, int attack, int defense, int speed)
    {
        characterName = name;
        ID = id;
        maxHP = hp;
        currentHP = hp;
        attackPower = attack;
        defensePower = defense;
        this.speed = speed;
    }
    public void TakeDamage(int damage)
    {
        int damageTaken = Mathf.Max(damage - defensePower, 0);
        currentHP -= damageTaken;
        currentHP = Mathf.Max(currentHP, 0);
        if (damageTaken > 0)
        {
            Debug.Log(characterName + "は" + damageTaken + "のダメージを受けた！");
        }
        else
        {
            Debug.Log(characterName + "はダメージを受けなかった！");
        }

    }

    public bool IsDead()
    {
        return currentHP <= 0;
    }
}
