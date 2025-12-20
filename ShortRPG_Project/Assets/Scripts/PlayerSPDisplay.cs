using UnityEngine;
using UnityEngine.UI;

public class PlayerSPDisplay : MonoBehaviour
{
    [SerializeField] BarUIDisplay spDisplay;
    [SerializeField] PlayerStatus player;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {  
       // spDisplay.status.OnSPRecover += DelayRecover;
       spDisplay.status=player.status;
    }

    // Update is called once per frame
    void Update()
    {
        spDisplay.main.fillAmount = (float)spDisplay.status.currentSP / spDisplay.status.maxSP;
      
      
    }
   
    public void DelayDamage()
    {
        var ratio= (float)spDisplay.status.currentSP / spDisplay.status.maxSP;
        spDisplay.SetDamage(ratio);
    }
    public void DelayRecover()
    {

        float ratio = (float)spDisplay.status.currentSP / spDisplay.status.maxSP;

        spDisplay.SetRecover(ratio);
    }
}
