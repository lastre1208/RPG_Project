using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject battleUI;
    public GameObject customUI;
    public GameObject resultUI;
    public SelectSkill select;
    public ResultDisplay result;
    public CustomManager custom;
    public void EnableBattleUI()
    {
        battleUI.SetActive(true);
        select.StartSelect();
    }

    public void DisableBattleUI()
    {
        battleUI.SetActive(false);
    }
   
    public void EnableResultUI()
    {
        resultUI.SetActive(true);
        result.StartResult();

    }
    public void DisableResultUI()
    {
        resultUI.SetActive(false);
        customUI.SetActive(true);
        custom.StartCustom();
    }

}
