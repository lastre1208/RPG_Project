using System.Linq;
using UnityEngine;

public class CommandSelect : MonoBehaviour//�R�}���h�{�^���������ꂽ�Ƃ��̏���
{
     BattleState battleStatus;
    public BattleManager manager;
    public SelectTarget t_select;
    public SkillExecuter skill;
    public SkillData attack;//�ʏ�U��
    public SkillData defense;//�h��

    private EnemyStatus target;
    public EnemyStatus Target
    {
        get { return target; }
        set { target = value; }
    }
    private SkillData selectSkill;
    public SkillData SelectSkill
    {
        get { return selectSkill; }
        set { selectSkill = value; }
    }
    public void Start()
    {
        battleStatus = manager.state;
    }

    public void OnAttackButton()//�ʏ�U��
    {

        if (battleStatus != BattleState.PLAYERTURN) return;//�v���C���[�̃^�[���łȂ���Ή������Ȃ�
        selectSkill = attack;
        t_select.TargetStart();
      
    }
  
    public void OnSkillButton()//�X�L������
    {
        if (battleStatus != BattleState.PLAYERTURN) return;

    }
    public void OnDefendButton()
    {
        if (battleStatus != BattleState.PLAYERTURN) return;
        skill.ExecuteSkill(manager.player.status,null,defense);
        manager.ExecuteAction();

    }
     public void PlayAction()
    {
        skill.ExecuteSkill(manager.player.status, target.status, selectSkill);
        manager.ExecuteAction();
    }

   
}
