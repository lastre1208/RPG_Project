using UnityEngine;
using System.Collections.Generic;
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
  
    
    public BattleState state=BattleState.START;
    public UIManager manager;

    public List<Enemy> enemies = new List<Enemy>();//�G�̃X�e�[�^�X�B�G���J�E���g���ɓǂݍ��ށB
    public PlayerStatus player;//�v���C���[�̃X�e�[�^�X�B
  
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
    public void EncounterEnemy()//�퓬�J�n���ɌĂԁB
    {
        SetTurn(BattleState.START);
    }

    public void StartBatlle()
    {
        Debug.Log("�퓬�J�n");
        foreach (Enemy enemy in enemies)
        {
            Debug.Log(enemy.status.status.characterName + "�������ꂽ�I");

        }
            
        
        SetTurn(BattleState.PLAYERTURN);
        Debug.Log("�v���C���[�̃^�[��");
        manager.EnableBattleUI();
       
    }

    public void PlayerTurn()
    {

      //  Debug.Log("�v���C���[�̃^�[��");
       
    }
   
 
   
    public void EnemyTurn()//�G�̎v�l
    {

        Debug.Log("�G�̃^�[��");
        ExecuteAction();

    }
   public void ExecuteAction()//�s�����s
    {
        if (CheckBattleEnd())
        {
            EndBattle();

        }
        else if(state==BattleState.PLAYERTURN)
        {
            SetTurn(BattleState.ENEMYTURN);
            
        }
        else
        {
            SetTurn(BattleState.PLAYERTURN);
            Debug.Log("�v���C���[�̃^�[��");
        }
    }

    public void EndBattle()
    {
        Debug.Log("�퓬�I��");
        manager.DisableBattleUI();
    
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
         int count = 0;
        foreach (Enemy status in enemies)
        {
           
            if (status.IsDead())//�G���S������ł��犮��
            {
                count++;

                if (count == enemies.Count)
                {
                    SetTurn(BattleState.WON);
                    Debug.Log("����");
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
