using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

[System.Serializable]
public enum SkillTypeImage
{
    Attack,
    Heal,
    Power_Up,
    Power_Down,
    Defense_Up,
    Defense_Down,
    Scale_Up,
    Scale_Down,
    Interval_Up,
    Interval_Down,
    Damage_Up, 
    Damage_Down,

    State_Panic,
    State_Sleep,
    State_Palayse,
    State_Mind
}
[System.Serializable]
public class ImageEntry
{
    public SkillTypeImage skillType;
    public Sprite iconSprite; 
}
[CreateAssetMenu(fileName ="SkillImageDatabase", menuName = "ScriptableObjects/SkillImage")]
public class SkillImageDatabase : ScriptableObject
{
    [SerializeField] List<ImageEntry> icons=new();

    private Dictionary<SkillTypeImage,Sprite> iconDict;


    private void OnEnable()
    {
        iconDict=new Dictionary<SkillTypeImage,Sprite>();

        foreach(var icon in icons)
        {
            iconDict.Add(icon.skillType, icon.iconSprite);
        }

    }
public Sprite SetSkillImage(SkillData skill)
    {
        Sprite sprite;

        switch (skill.skillType)
        {
            case SkillType.Attack:
                {

                    iconDict.TryGetValue(SkillTypeImage.Attack ,out  sprite);


                    return sprite;
                }


        }



        iconDict.TryGetValue(SkillTypeImage.Attack, out  sprite);

        return sprite;

    }
    public string SetSkillNum(SkillData skill,Enemy enemy) { 
   

        switch (skill.skillType)
        {
            case SkillType.Attack:
                {
                    AttackSkill attack=skill as AttackSkill;

                    int damage =(int)((enemy.commonStatus.attackPower * enemy.commonStatus.damageRatio * attack.attackRatio)-enemy.Player.status.defensePower);

                    if (damage < 0)
                    {

                        damage = 0;
                    }
                   return (damage).ToString();


                }
        }
        return ("");
    
    }
}
