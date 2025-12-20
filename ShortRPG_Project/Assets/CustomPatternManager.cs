using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]



public enum CustomRank
{
    Common,
    Rare,
    Super,
    Expert,


}
[System.Serializable]
public enum Custom
{
    Interval,
    Scale,
    Attack,

}
[System.Serializable]

public class CustomEffect
{
    public Custom Type;
    public float influenceRate;//能力の上げる基準
}
[System.Serializable]
public class CustomData
{

    public string name;
    public string description;
    
    public List<CustomEffect> effects;
    public SkillData getSkill;//獲得できるスキル
   
}
[System.Serializable]
public class CustomPattern
{
    public CustomRank rank;
    public List<CustomData> data;

}
public class CustomPatternManager : MonoBehaviour
{
    [SerializeField] List<CustomPattern> patterns;
    [SerializeField] List<Button> selectButton;
    [SerializeField] List<TMP_Text> description_Text;
    [SerializeField] List<TMP_Text> skill_Text;
    HashSet<CustomData> data;
    private void Start()
    {
         data = new HashSet<CustomData>();
    }

    public void SetCustom(CustomRank rank)
    {


        List<CustomData> selectableList = new();

        foreach (CustomPattern pattern in patterns)
        {
            if (pattern.rank == rank)
            {
                foreach (CustomData item in pattern.data)
                {

                    if (!data.Contains(item))
                    {
                        selectableList.Add(item);
                    }


                }

            }
        }
            int needCustom = Math.Min(selectableList.Count,selectButton.Count);
            selectableList = selectableList.OrderBy(a => Guid.NewGuid()).ToList();


        for (int i = 0; i < selectButton.Count; i++)
        {
           
            
              var d_text = description_Text[i];
                var s_text = skill_Text[i];
                var button = selectButton[i];
            
            
            if (selectableList.Count > i)
            {

                data.Add(selectableList[i]);

                d_text.text = selectableList[i].name + "\n" + selectableList[i].description;
                s_text.text = "獲得スキル\n" + selectableList[i].getSkill.skillName;

                CustomButtonHandler handler = button.GetComponent<CustomButtonHandler>();

                button.interactable = true;

                handler.data = selectableList[i];
              
            }
            else
            {
                d_text.text = "None";
                s_text.text = "";
                
                button.interactable = false;

            }
        }


        }
    }
  

