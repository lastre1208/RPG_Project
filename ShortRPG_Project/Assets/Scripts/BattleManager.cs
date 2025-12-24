using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.U2D.Aseprite;
public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    STARTTURN,
    ENDTURN,
    WON,
    LOST
}

public class BattleManager : MonoBehaviour//戦闘の一連の流れを管理するクラス
{

    public WinDisplay win;
    public BattleState state = BattleState.START;
    public UIManager manager;
    public SkillExecuter skill;
    public List<Enemy> enemies = new List<Enemy>();//敵のステータス。エンカウント時に読み込む。
    public PlayerStatus player;//プレイヤーのステータス。
    public EncountManager encountManager;
   // public CustomManager customManager;
    public BattleProductManager productManager;
    public HitDisplay hitDisplay;
    public int hitDevidePoint;
    public int turnSkillPoint = 20;
    public int decreaseSkillPoint = 5;
    public bool isBattle = true;
    private int level = 0;
    public CustomRank rank;
    public int getSkillPoint;
    public int enemyGetPoint;
    public int Level
    {

        get { return level; }
        set { level = value; }
    }
    private void Update()
    {
        if (isBattle)
            switch (state)//START→STARTTURN→PLAYER(敵全滅でWONへ移行)→ENEMY(プレイヤー死亡でLOSTへ移行)→END→STARTTURNへ
            {
                case BattleState.START:
                    {

                        StartBatlle();
                        break;
                    }
                case BattleState.PLAYERTURN:
                    {

                        PlayerTurn();
                        break;
                    }

                case BattleState.ENEMYTURN:
                    {
                        EnemyTurn();
                        break;
                    }
                case BattleState.STARTTURN://ターンのはじめ
                    {
                        EnemySetUp();
                        player.skillCount = 3;
                        player.ResetHit();
                        manager.EnableBattleUI();

                        SetTurn(BattleState.PLAYERTURN);

                        break;
                    }
                case BattleState.ENDTURN://ターンの終わり
                    {
                        player.status.SPHeal(player.recoverSp);
                        player.status.UpdateBuffsPerTurn();

                        skill.ResetSkillCounts();

                        getSkillPoint -= decreaseSkillPoint;
                        if(getSkillPoint < 0)getSkillPoint = 0;

                        foreach (Enemy enemy in enemies)
                        {

                            enemy.commonStatus.UpdateBuffsPerTurn();
                        }
                        SetTurn(BattleState.STARTTURN);
                        break;
                    }

                case BattleState.WON:
                    {

                        StartCoroutine(win.WinCoroutine());
                          GetSkillPoints();
                        manager.EnableResultUI();
                     ResetPoints();
                       
                        player.ReturnDefault(player);
                        hitDisplay.StopHit();
                      
                        isBattle = false;
                        break;
                    }
                case BattleState.LOST:
                    {
                        break;
                    }

            }
    }
    public void EncounterEnemy()//戦闘開始時に呼ぶ。
    {
        enemies = encountManager.EncountEnemy(Level, rank);
        SetTurn(BattleState.START);
    }

    public void SetBattle()
    {
        isBattle = true;
        SetTurn(BattleState.START);


    }

    public void StartBatlle()
    {
        EncounterEnemy();
        player.SetDefault(player.status);
        getSkillPoint = turnSkillPoint;
        Debug.Log("戦闘開始");
        foreach (Enemy enemy in enemies)
        {
            Debug.Log(enemy.commonStatus.characterName + "があらわれた！");

        }


        SetTurn(BattleState.STARTTURN);
        Debug.Log("プレイヤーのターン");
        manager.EnableBattleUI();

    }

    public void PlayerTurn()
    {


        //  Debug.Log("プレイヤーのターン");

    }

    public void EnemySetUp()//発動するスキルの選択とダメージ減衰率の回復
    {
        foreach (Enemy enemy in enemies)
        {
            if (!enemy.commonStatus.IsDead() || !enemy.IsActive)
            {
                enemy.nextSkill = enemy.SkillAndTarget.SelectSkill(enemy);
                enemy.DecayDamage.RecoverDecay(enemy);
                Debug.Log(enemy.nextSkill);
            }


        }


    }

    public void EnemyTurn()//敵の行動
    {

        Debug.Log("敵のターン");

        foreach (Enemy enemy in enemies)
        {
            if (!enemy.commonStatus.IsDead() || !enemy.IsActive)
            {

                if (enemy.nextSkill.isAllTarget)
                {
                    foreach (Enemy targetenemy in enemies)
                    {
                        if (!targetenemy.commonStatus.IsDead())
                        {
                            skill.ExecuteSkill(enemy.nextSkill, enemy.commonStatus, targetenemy.status.status);

                        }

                    }


                }
                else
                {
                    var target = enemy.SkillAndTarget.SelectTarget(enemy.nextSkill, player.status, enemy.commonStatus);
                    skill.ExecuteSkill(enemy.nextSkill, enemy.commonStatus, target);
                }

            }


        }
        ExecuteAction();

    }
    public void ExecuteAction()//行動実行。プレイヤー→敵→ターン更新の順番で処理される。
    {
        JudgeEnd();
        if (state == BattleState.PLAYERTURN)
        {
            SetTurn(BattleState.ENEMYTURN);
        }
        else if (state == BattleState.ENEMYTURN)
        {
            SetTurn(BattleState.ENDTURN);
            Debug.Log("プレイヤーのターン");
        }
    }

    public void JudgeEnd()
    {
        if (CheckBattleEnd())
        {
            EndBattle();

        }
    }
    public void EndBattle()
    {
        Debug.Log("戦闘終了");
        // StopAllCoroutines();
        skill.ResetSkillCounts();
        manager.DisableBattleUI();


    }
    public void SetTurn(BattleState state)
    {
        this.state = state;
    }
    public bool CheckBattleEnd()
    {
        if (player.status.IsDead())
        {
            SetTurn(BattleState.LOST);
            Debug.Log("敗北");
            return true;

        }
        else if (EnemyDeadCheck())
        {
            SetTurn(BattleState.WON);
            Debug.Log("勝利");
            return true;

        }
        return false;
    }

    public bool EnemyDeadCheck()
    {
        var count = 0;
        foreach (Enemy status in enemies)
        {
            if (status.commonStatus.IsDead())
            {
                count++;
            }
        }
        return count == enemies.Count;
    }
    public void GetSkillPoints()
    {
        foreach (Enemy status in enemies)
        {
          
            enemyGetPoint += status.exp;
        }
        player.skillPoints += enemyGetPoint;
        player.skillPoints += player.hitsumCount/hitDevidePoint;
        player.skillPoints += player.maxHitCount;
        player.skillPoints += getSkillPoint; 
        
        
       
       
    }
    public void ResetPoints()
    {
        player.hitsumCount = 0;
        player.hitCount = 0;
        enemyGetPoint = 0;
    }
}
