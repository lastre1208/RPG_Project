using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerStatus : MonoBehaviour
{
    public CommonStatus status;
    public Weapon equippedWeapon;
    public List<Weapon> havedWeapon;
    public float shotTime;
    public int exp;
    public int nextExp;
    public int level;

    public bool IsDead()
    {

        return status.currentHP <= 0;
    }
}
