using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject battleUI;
    public GameObject targetUI;

    public void EnableBattleUI()
    {
        battleUI.SetActive(true);
    }

    public void DisableBattleUI()
    {
        battleUI.SetActive(false);
    }
    public void EnableTargetUI()
    {
        targetUI.SetActive(true);
    }
    public void DisableTargetUI()
    {
        targetUI.SetActive(false);
    }

}
