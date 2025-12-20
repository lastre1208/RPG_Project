using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System;

public class CustomManager : MonoBehaviour//戦闘終了後に呼ぶ。強化フェーズ。
{
    [SerializeField] GameObject CustomUI;
    [SerializeField]CustomSelectManager customSelectManager;
    public PlayerStatus player;


[System.Serializable]
public enum CustomType{

    HP,
    SP,
    Attack,
    Defence


}

    public Dictionary<CustomType, int> customPoints=new();
    public void Start()
    {
        CustomUI.SetActive(false);
        foreach (CustomType type in Enum.GetValues(typeof(CustomType)))
        {

            customPoints[type] = 0;

        }

    }


    //public void IncreasePoint(CustomType type)
    //{

    //   customPoints[type] += 1;

    //    player.skillPoints--;
    //}
    //public void DecreasePoint(CustomType type)
    //{
    //    customPoints[type] -= 1;

    //    player.skillPoints++;
    //}
    public void StartCustom()
    {
        CustomUI.SetActive(true);
        foreach (CustomType type in Enum.GetValues(typeof(CustomType)))
        {

            customPoints[type] = 0;

        }
    
    }


    public void EndCustom()
    {

        foreach (CustomType type in Enum.GetValues(typeof(CustomType)))
        {


            if (type == CustomType.HP) player.status.maxHP += customPoints[type];
            else if (type == CustomType.SP) player.status.maxSP += customPoints[type];
            else if (type == CustomType.Attack) player.status.attackPower += customPoints[type];
            else if (type == CustomType.Defence) player.status.defensePower += customPoints[type];


        }

        CustomUI.SetActive(false);
        customSelectManager.CustomSelectStart();
        player.SetDefault(player.status);
    }
}
