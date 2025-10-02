using UnityEngine;
public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}

public class BattleManager : MonoBehaviour//�퓬�̈�A�̗�����Ǘ�����N���X
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
        Debug.Log("�퓬�J�n");

        Debug.Log(enemy.characterName + "�������ꂽ�I");
    }

    public void OnAttackButton()
    {

        if (state != BattleState.PLAYERTURN) return;//�v���C���[�̃^�[���łȂ���Ή������Ȃ�

        ICommand attackCommand = new AttackCommand();
  Debug.Log("�v���C���[�̍U��");
        attackCommand.Execute(player, enemy);

      
        if (!CheckBattleEnd())//�I���Ȃ�������G�̔�
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
        Debug.Log("�G�̍U��");
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
        Debug.Log("�퓬�I��");
        
    
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
            Debug.Log("�s�k");
            return true;

        }
        else if (enemy.IsDead())
        {
            SetTurn(BattleState.WON);
            Debug.Log("����");
            return true;
        }
        return false ;
    }
}
