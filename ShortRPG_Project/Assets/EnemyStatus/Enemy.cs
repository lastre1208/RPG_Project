using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DefaultEnemyStatus
{
    public float defaultDamageRate;
    public float defaultDacayDamage;
    public float defaultEasyDecay;
    public float defaultRecover;
}
public class Enemy : MonoBehaviour,ICharacterSet, IBuffEffect
{
    public EnemyStatus status;
    public TMP_Text nameText;
    public SpriteRenderer characterImage;
    public TMP_Text damageText;

    public float damageRate;
    public float DacayDamage;
    public float easyDecay;
    public float recover;

    int actionNum = 0;
    bool isDamage;
    float countTime;
    DefaultEnemyStatus defaultEnemy = new();
    PlayerStatus player;

   
    private void Start()
    {
        status.status.Inject(this, this);
 
        
        characterImage=this.GetComponent<SpriteRenderer>();
        characterImage.sprite = status.status.characterImage;
        TMP_Text[]TMP_Texts= this.GetComponentsInChildren<TMP_Text>();

        nameText = TMP_Texts[0];
        nameText.text = status.status.characterName;
        damageText=TMP_Texts[1];

        
        status.status.currentHP = status.status.maxHP;
        status.status.currentSP = status.status.maxSP;

       
        damageRate=status.takedamageRatio;
        easyDecay=status.easydecayDamage;
        DacayDamage =status.decayDamageRatioLimit;
        recover =status.recoverDamageRatio;

        player = GameObject.FindWithTag("Player").GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        characterImage.transform.position = this.transform.position;
        if (isDamage)
        {
            countTime += Time.deltaTime;

            if(countTime >= 0.5f)
            {
                damageText.text = "";
                isDamage = false;
                countTime = 0f;
            }
        }
    }

    public void SetDefault(CommonStatus common)
    {
        common.attackPower = status.status.attackPower;
        common.defensePower = status.status.defensePower;
        common.maxHP = status.status.maxHP;
        common.maxSP = status.status.maxSP;
        common.damageRatio = status.status.damageRatio;


        defaultEnemy.defaultDamageRate = damageRate;
        defaultEnemy.defaultDacayDamage  = DacayDamage;
        defaultEnemy.defaultEasyDecay = easyDecay;
        defaultEnemy.defaultRecover = recover;
    }
    public void ApplyBuffEffect(List<BuffEntry> buffs)
    {

        foreach (var buff in buffs)
        {
            switch (buff.status)
            {
                case ModifyStatus.Power:
                    status.status.attackPower *= buff.ratio;
                    break;
                case ModifyStatus.Defence:
                    status.status.defensePower *= buff.ratio;
                    break;
               
                    // 必要に応じて追加
            }
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)//攻撃を受けるとき
    {
        if (collision.gameObject.tag == "PlayerAttack")
        {
            countTime = 0;
            Debug.Log("aaa");
            status.status.TakeDamage(collision.gameObject.GetComponent<Weapon>().attackPower+(int)player.status.attackPower);
            damageText.text = (collision.gameObject.GetComponent<Weapon>().attackPower+ (int)player.status.attackPower).ToString();
            isDamage = true;
        }

    }
    public CommonStatus SelectTarget(SkillData skill)//スキルごとにターゲットが変わる
    {
        CommonStatus target;

        switch (skill.skillType)
        {
            case SkillType.Attack:
                {

                    target = player.status;
                    return target;
                }
            case SkillType.Heal:
                {

                    target = this.status.status;
                    return target;
                }
            case SkillType.Defence:
                {

                    target = this.status.status;
                    return target;
                }
            case SkillType.Buff:
                {
                    target = this.status.status;
                    return target;
                }
            case SkillType.Debuff:
                {
                    target = player.status
                      ;
                    return target;
                }

        }




        return null;
    }
    public SkillData SelectSkill(EnemyStatus status)

        
    {
        switch (status.intelligence)
        {
            case
                EnemyStatus.Intelligence.Fool://ランダムに選出
                {
                    int random =Random.Range(0, status.status.skillData.Count);

                    return status.status.skillData[random];
                  
                }
            case EnemyStatus.Intelligence.Normal://順番通り
                {
                    actionNum++; 
                    return status.status.skillData[actionNum%status.status.skillData.Count];
                   
                    
                }
            case EnemyStatus.Intelligence.Smart://プレイヤーが嫌がるものを選出
                {
                    break;
                }
        }

        return null;
    }
}
