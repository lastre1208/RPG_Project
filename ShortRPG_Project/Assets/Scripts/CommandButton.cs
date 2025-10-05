using System.Linq;
using UnityEngine;

public class CommandSelect : MonoBehaviour//コマンドボタンが押されたときの処理
{
     BattleState battleStatus;
    public BattleManager manager;
    public SelectTarget t_select;
    public SkillExecuter skill;
    public SkillData attack;//通常攻撃
    public SkillData defense;//防御

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

    public void OnAttackButton()//通常攻撃
    {

        if (battleStatus != BattleState.PLAYERTURN) return;//プレイヤーのターンでなければ何もしない
        selectSkill = attack;
        t_select.TargetStart();
      
    }
  
    public void OnSkillButton()//スキル発動
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
