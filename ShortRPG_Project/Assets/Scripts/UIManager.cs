using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject battleUI;
  
    public void EnableBattleUI()
    {
        battleUI.SetActive(true);
    }

    public void DisableBattleUI()
    {
        battleUI.SetActive(false);
    }
   

}
