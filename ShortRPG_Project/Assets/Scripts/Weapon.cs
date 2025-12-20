using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
   
    public PlayerStatus player;
    public WeaponData weapon;
    public Sprite icon;
    public string weaponName;
    public int attackPower;
    public float fireInterval;
    public float bulletScale = 1f;

    private void Start()
    {
               icon = weapon.icon;
        weaponName = weapon.weaponName;
        attackPower = weapon.attackPower;
        fireInterval = weapon.fireInterval;
        bulletScale = weapon.bulletScale;

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.AddHit();
       
        player.isHit = true;
    }



}
