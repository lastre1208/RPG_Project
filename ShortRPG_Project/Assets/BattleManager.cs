using UnityEngine;
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
    public CharacterStatus player;
    public CharacterStatus enemy;
    
    public BattleState state=BattleState.PLAYERTURN;
    public void Start()
    {
        StartBatlle();
    }

    public void StartBatlle()
    {
        Debug.Log("戦闘開始");

        Debug.Log(enemy.characterName + "があらわれた！");
    }

    public void OnAttackButton()
    {

        if (state != BattleState.PLAYERTURN) return;//プレイヤーのターンでなければ何もしない

        ICommand attackCommand = new AttackCommand();
  Debug.Log("プレイヤーの攻撃");
        attackCommand.Execute(player, enemy);

      
        if (!CheckBattleEnd())//終わらなかったら敵の番
        {
            SetTurn(BattleState.ENEMYTURN); 
            EnemyAttack();
        }
        else
        {

            EndBattle();
           
        }
        
    }


    public void EnemyAttack()
    {
        if (state != BattleState.ENEMYTURN) return;

        ICommand attackCommand = new AttackCommand();
        Debug.Log("敵の攻撃");
        attackCommand.Execute(enemy, player);

        
       
        if (!CheckBattleEnd())
        {
            SetTurn(BattleState.PLAYERTURN);
        }
        else
        {
            EndBattle();
        }
    }

    public void EndBattle()
    {
        Debug.Log("戦闘終了");
        
    
    }


    public void SetTurn(BattleState state)
    {
        this.state = state;
    }
    public bool CheckBattleEnd()
    {
        if (player.IsDead())
        {
            SetTurn(BattleState.LOST);
            Debug.Log("敗北");
            return true;

        }
        else if (enemy.IsDead())
        {
            SetTurn(BattleState.WON);
            Debug.Log("勝利");
            return true;
        }
        return false ;
    }
}
