using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]WeaponData weapon;
    public Sprite icon;
    public string weaponName;
    public int attackPower;
    public float fireInterval;
    public int bulletCount;

    private void Start()
    {
               icon = weapon.icon;
        weaponName = weapon.weaponName;
        attackPower = weapon.attackPower;
        fireInterval = weapon.fireInterval;
        bulletCount = weapon.bulletCount;
    }
}
