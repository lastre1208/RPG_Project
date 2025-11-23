using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
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
    public CommonStatus commonStatus;
    public TMP_Text nameText;
    public SpriteRenderer characterImage;
    public TMP_Text damageText;
     
    public float damageRate;
    public float DacayDamage;
    public float easyDecay;
    public float recover;
    public event Action<int> OnDamaged;
    int actionNum = 0;
 private   bool isDamage;
    public bool IsDamage
    {
        get { return isDamage; }
       
    }
  private  float countTime;
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

            if(countTime >= 0.5f)
            {
               
                isDamage = false;
                countTime = 0f;
            }
        }
    }
    public void Init(EnemyStatus enemyStatus)
    {
        status = enemyStatus;
        status.status.Inject(this, this);
        commonStatus = status.status;
      
       
        // 見た目・UIの初期化
        characterImage = this.GetComponent<SpriteRenderer>();
        characterImage.sprite = status.status.characterImage; 
        
        
        polygonCollider=this.GetComponent<PolygonCollider2D>();
        ResetPolygonShape(polygonCollider, characterImage);
        
        TMP_Text[] TMP_Texts = this.GetComponentsInChildren<TMP_Text>();
        nameText = TMP_Texts[0];
        nameText.text = status.status.characterName;
        damageText = TMP_Texts[1];

        skillAndTarget = this.GetComponent<EnemySelectSkillAndTarget>();

        // ステータス初期値
        status.status.currentHP = status.status.maxHP;
        status.status.currentSP = status.status.maxSP;

        // その他パラメータ初期値
        damageRate = status.takedamageRatio;
        easyDecay = status.easydecayDamage;
        DacayDamage = status.decayDamageRatioLimit;
        recover = status.recoverDamageRatio;

        // プレイヤー情報取得
        player = GameObject.FindWithTag("Player").GetComponent<PlayerStatus>();
    }
  
    public void ResetPolygonShape(PolygonCollider2D polygon,SpriteRenderer sprite)
    {
        if (polygon == null || sprite == null || sprite.sprite == null) return; 


        int shapeCount=sprite.sprite.GetPhysicsShapeCount();
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
            var damage = collision.gameObject.GetComponent<Weapon>().attackPower + (int)player.status.attackPower-(int)status.status.defensePower;

            OnDamaged?.Invoke(damage);
          status.status.TakeDamage(damage);
            
            isDamage = true;
        }

    }
}
