using UnityEngine;

public class CustomSelectManager : MonoBehaviour
{
    [SerializeField] GameObject selectUI;
    [SerializeField]CustomPatternManager patternManager;
    [SerializeField] BattleManager battleManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectUI.SetActive(false);
    }


    public void CustomSelectStart()
    {

        selectUI.SetActive(true);
        patternManager.SetCustom(battleManager.rank);

    }

    public void CustomSelectEnd()
    {
        selectUI.SetActive(false);
        battleManager.SetBattle();
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
