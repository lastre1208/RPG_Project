using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class DefaultStatus
{
    public float power;
        public float defense;
    public float interval;
    public float time;
    public float size;
}
public class StatusDisplay : MonoBehaviour
{
    public GameObject character;

    public TMP_Text hpText;
    public TMP_Text mpText;

    public TMP_Text powerText;
    public TMP_Text defenseText;
    public TMP_Text shotIntervalText;
    public TMP_Text shotTimeText;
    public TMP_Text bulletSizeText;


    public TMP_Text levelText;
    public TMP_Text expText;
    public TMP_Text nextLevelText;

    PlayerStatus status;

    DefaultStatus defaultStatus=new();

    [SerializeField] Color UpColor;
    [SerializeField] Color DownColor;
    void Start()
    {
       
        
        status = character.GetComponent<PlayerStatus>();
        status.status=character.GetComponent<PlayerStatus>().status;
        DefaultSet();
    }
    public void DefaultSet()
    {
        defaultStatus.power = status.status.attackPower;
        defaultStatus.defense = status.status.defensePower;
        defaultStatus.interval = status.intervalRatio;
        defaultStatus.time = status.shotTime;
        defaultStatus.size = status.bulletScale;

    }

    // Update is called once per frame
    void Update()
    {

        hpText.text = (status.status.currentHP + "/" + status.status.maxHP);
        mpText.text = (status.status.currentSP + "/" + status.status.maxSP);


        CheckDefault(powerText, status.status.attackPower, defaultStatus.power);
        CheckDefault(defenseText, status.status.defensePower, defaultStatus.defense);
        CheckDefault(bulletSizeText, status.bulletScale, defaultStatus.size);
        CheckDefault(shotIntervalText, status.intervalRatio, defaultStatus.interval);
        CheckDefault(shotTimeText, status.shotTime, defaultStatus.time);

       

        levelText.text=status.level.ToString();
        expText.text =  status.exp.ToString();
        nextLevelText.text =status.nextExp.ToString();
    }

    public void CheckDefault<T>(TMP_Text text, T currentValue, T defaultValue)
        where T : System.IComparable<T>
    {

        text.text = currentValue.ToString();


        if (currentValue.CompareTo(defaultValue) > 0)
        {
            text.color = UpColor;
        }
        else if (currentValue.CompareTo(defaultValue) < 0)
        {
            text.color = DownColor;
        }
        else
        {
          //  Debug.Log("???");
            text.color = Color.white;
        }
    }
}
