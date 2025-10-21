using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData")]

[System.Serializable]
public class WeaponData : ScriptableObject
{
    public Sprite icon;
    public string weaponName;
    public int attackPower;
    public float fireInterval;
    public int bulletCount;
}
