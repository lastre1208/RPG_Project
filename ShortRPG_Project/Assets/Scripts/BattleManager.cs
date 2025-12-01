using UnityEngine;
using System.Collections.Generic;
using System.Linq;
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
  
    
    public BattleState state=BattleState.START;
    public UIManager manager;
    public SkillExecuter skill;
    public List<Enemy> enemies = new List<Enemy>();//敵のステータス。エンカウント時に読み込む。
    public PlayerStatus player;//プレイヤーのステータス。
  public EncountManager encountManager;

    private int Level = 0;

    private void Update()
    {
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
                    SetTurn(BattleState.PLAYERTURN);

                    break;
                }
            case BattleState.ENDTURN://ターンの終わり
                {

                    player.status.UpdateBuffsPerTurn();
                    foreach (Enemy enemy in enemies)
                    {

                        enemy.commonStatus.UpdateBuffsPerTurn();
                    }
                    SetTurn(BattleState.STARTTURN);
                    break;
                }

            case BattleState.WON:
                {
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
      enemies= encountManager.EncountEnemy(Level);
        SetTurn(BattleState.START);
    }

    public void StartBatlle()
    {
        EncounterEnemy();
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
            if (!enemy.commonStatus.IsDead()||!enemy.IsActive)
            {
               // enemy.nextSkill = enemy.SkillAndTarget.SelectSkill(enemy);
                if (enemy.nextSkill != null)
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


        } ExecuteAction();

    }
   public void ExecuteAction()//行動実行。プレイヤー→敵→ターン更新の順番で処理される。
    {
        if (CheckBattleEnd())
        {
            EndBattle();

        }
        else if(state==BattleState.PLAYERTURN)
        {
           
            SetTurn(BattleState.ENEMYTURN);
            
        }
        else if(state==BattleState.ENEMYTURN)
        {
            SetTurn(BattleState.ENDTURN);

         
            Debug.Log("プレイヤーのターン");
        }
    }

    public void EndBattle()
    {
        Debug.Log("戦闘終了");
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
}
