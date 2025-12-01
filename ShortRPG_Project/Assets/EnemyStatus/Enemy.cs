using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using System.ComponentModel;
public class DefaultEnemyStatus
{
    public Intelligence defaultIntelligence;
    public float defaultDamageRate;
    public float defaultDacayDamage;
    public float defaultEasyDecay;
    public float defaultRecover;
}
public class Enemy : MonoBehaviour, ICharacterSet, IBuffEffect, IDebuffEffect
{
    public EnemyStatus status;
    public CommonStatus commonStatus;
    public TMP_Text nameText;
    public SpriteRenderer characterImage;

    public SkillData nextSkill;
    public Intelligence Intelligence;
    public int exp;
    public float damageRate;
    public float DacayDamage;
    public float easyDecay;
    public float recover;
  

    private bool isActive = true;

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    private bool isDamage;
    public bool IsDamage
    {
        get { return isDamage; }

    }
    private float countTime;
    public float CountTime
    {
        get { return countTime; }
    }
    DefaultEnemyStatus defaultEnemy = new();
    PlayerStatus player;
    PolygonCollider2D polygonCollider;
    private EnemySelectSkillAndTarget skillAndTarget;
    public EnemySelectSkillAndTarget SkillAndTarget
    {
        get { return skillAndTarget; }
    }

    private void Update()
    {
        characterImage.transform.position = this.transform.position;
        if (isDamage)
        {
            countTime += Time.deltaTime;

            if (countTime >= 0.5f)
            {

                isDamage = false;
                countTime = 0f;
            }
        }
    }
    public void Init(EnemyStatus enemyStatus)
    {
        status = enemyStatus;
        status.status.Inject(this, this, this);
        // commonStatus = status.status;
        SetCommon(commonStatus);

        // 見た目・UIの初期化
        characterImage = this.GetComponent<SpriteRenderer>();
        characterImage.sprite = status.status.characterImage;


        polygonCollider = this.GetComponent<PolygonCollider2D>();
        ResetPolygonShape(polygonCollider, characterImage);

        TMP_Text TMP_Texts = this.GetComponentInChildren<TMP_Text>();
        nameText = TMP_Texts;
        nameText.text = status.status.characterName;


        skillAndTarget = this.GetComponent<EnemySelectSkillAndTarget>();

        // ステータス初期値
        // status.status.currentHP = status.status.maxHP;
        // status.status.currentSP = status.status.maxSP;

        // その他パラメータ初期値
        exp = status.exp;
        damageRate = status.takedamageRatio;
        easyDecay = status.easydecayDamage;
        DacayDamage = status.decayDamageRatioLimit;
        recover = status.recoverDamageRatio;

        // プレイヤー情報取得
        player = GameObject.FindWithTag("Player").GetComponent<PlayerStatus>();
    }

    public void SetCommon(CommonStatus commonStatus)
    {
        commonStatus.characterName=status.status.characterName;
        commonStatus.skillData = status.status.skillData;
        commonStatus.attackPower = status.status.attackPower;
        commonStatus.defensePower = status.status.defensePower;
        commonStatus.maxHP = status.status.maxHP;
        commonStatus.currentHP = commonStatus.maxHP;
        commonStatus.maxSP = status.status.maxSP;
        commonStatus.currentSP = status.status.maxSP;
        commonStatus.damageRatio = status.status.damageRatio;
    }
    public void ResetPolygonShape(PolygonCollider2D polygon, SpriteRenderer sprite)
    {
        if (polygon == null || sprite == null || sprite.sprite == null) return;


        int shapeCount = sprite.sprite.GetPhysicsShapeCount();
        polygon.pathCount = shapeCount;

        for (int i = 0; i < shapeCount; i++)
        {
            List<Vector2> path = new List<Vector2>();
            sprite.sprite.GetPhysicsShape(i, path);

            polygon.SetPath(i, path);

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
        defaultEnemy.defaultDacayDamage = DacayDamage;
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
                    commonStatus.attackPower *= buff.ratio;
                    Mathf.Ceil(commonStatus.attackPower);
                    break;
                case ModifyStatus.Defence:
                    commonStatus.defensePower *= buff.ratio;
                    Mathf.Ceil(commonStatus.defensePower);
                    break;

                    // 必要に応じて追加
            }
        }
    }


    public void ApplyDebuffEffect(List<DebuffEntry> debuffs)
    {

        isActive = true;

        foreach (var debuff in debuffs)
        {
            switch (debuff.enableState)
            {

                case EnableState.Sleep://行動不能。攻撃を受けたら解除
                    {
                        isActive = false;
                        break;
                    }
                case EnableState.Palysis://50%で行動不能
                    {
                        if (isActive)//既に他の状態異常で休みになっている場合はスキップ
                        {
                            var rand = UnityEngine.Random.Range(0, 1);
                            isActive = rand == 1;


                        }

                        break;
                    }
                case EnableState.Panic://ランダムな行動
                    {

                        break;
                    }
                case EnableState.Mind://1ターン行動不能
                    {
                        isActive = false;
                        break;
                    }
            }

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)//攻撃を受けるとき
    {
        if (collision.gameObject.CompareTag("PlayerAttack") && !commonStatus.IsDead())
        {
            countTime = 0;
            var damage = collision.gameObject.GetComponent<Weapon>().attackPower + (int)player.status.attackPower - (int)commonStatus.defensePower;
            commonStatus.damageData.hitPosition=collision.gameObject.transform.position;
     
            commonStatus.TakeDamage(damage);

            isDamage = true;
        }

    }
}


  

