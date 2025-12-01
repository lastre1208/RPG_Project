using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject battleUI;
    public SelectSkill select;
    public void EnableBattleUI()
    {
        battleUI.SetActive(true);
        select.StartSelect();
    }

    public void DisableBattleUI()
    {
        battleUI.SetActive(false);
    }
   

}
