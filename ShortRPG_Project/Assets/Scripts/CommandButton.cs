using System.Collections;
using System.Linq;
using UnityEngine;

public class CommandSelect : MonoBehaviour//コマンドボタンが押されたときの処理
{
    public GameObject selectUI;
    public GameObject subStatusUI;
    public BattleManager manager;
   // public SelectTarget t_select;
    public SkillExecuter skill;
  public ShotExecuter shot;
    public SelectSkill select;
   // private Enemy target;
    //public Enemy Target
    //{
    //    get { return target; }
    //    set { target = value; }
    //}
    private SkillData selectSkill;
    public SkillData SelectSkill
    {
        get { return selectSkill; }
        set { selectSkill = value; }
    }
    public void Start()
    {
      
    }

    public void OnShotButton()//通常攻撃
    {

        if (manager.state != BattleState.PLAYERTURN) return;//プレイヤーのターンでなければ何もしない
        StartCoroutine(ShotCoroutine());

    }
    public IEnumerator ShotCoroutine()
    {
        selectUI.SetActive(false);
        subStatusUI.SetActive(false);
        yield return StartCoroutine(shot.StartShot(manager.player));
        PlayAction();
        subStatusUI.SetActive(true);
     //   selectUI.SetActive(true);
    }
    public void OnSkillButton()//スキル発動
    {
        if (manager.state != BattleState.PLAYERTURN) return;
        select.StartSelect();

    }
 
     public void PlayAction()
    {
      
        manager.ExecuteAction();
    }

   
}
