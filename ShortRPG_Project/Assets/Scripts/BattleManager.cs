using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
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
  
    private void Update()
    {
        switch (state)
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
        SetTurn(BattleState.START);
    }

    public void StartBatlle()
    {
        Debug.Log("戦闘開始");
        foreach (Enemy enemy in enemies)
        {
            Debug.Log(enemy.status.status.characterName + "があらわれた！");

        }
            
        
        SetTurn(BattleState.PLAYERTURN);
        Debug.Log("プレイヤーのターン");
        manager.EnableBattleUI();
       
    }

    public void PlayerTurn()
    {

      //  Debug.Log("プレイヤーのターン");
       
    }
   
 
   
    public void EnemyTurn()//敵の思考
    {

        Debug.Log("敵のターン");
       
        foreach (Enemy enemy in enemies)
        {
            if (!enemy.status.status.IsDead())
            {
                SkillData selectSkill = enemy.SelectSkill(enemy.status);
                if (selectSkill.isAllTarget)
                {
                    foreach (Enemy targetenemy in enemies)
                    {
                        skill.ExecuteSkill(selectSkill, enemy.status.status, targetenemy.status.status);
                    }
                  

                }
                else
                {
                    skill.ExecuteSkill(selectSkill, enemy.status.status, enemy.SelectTarget(selectSkill));
                }
                   
            }


        } ExecuteAction();

    }
   public void ExecuteAction()//行動実行
    {
        if (CheckBattleEnd())
        {
            EndBattle();

        }
        else if(state==BattleState.PLAYERTURN)
        {
            player.status.UpdateBuffsPerTurn();
            SetTurn(BattleState.ENEMYTURN);
            
        }
        else
        {
            SetTurn(BattleState.PLAYERTURN);

            foreach (Enemy enemy in enemies)
            {

                enemy.status.status.UpdateBuffsPerTurn();
            }
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
         int count = 0;
        foreach (Enemy status in enemies)
        {
           
            if (status.status.status.IsDead())//敵が全員死んでたら完了
            {
                count++;

                if (count == enemies.Count)
                {
                    SetTurn(BattleState.WON);
                    Debug.Log("勝利");
                    return true;
                }


            }
            else
            {
                return false;
            }
                
        }
        return false;
        
        
    }
}
