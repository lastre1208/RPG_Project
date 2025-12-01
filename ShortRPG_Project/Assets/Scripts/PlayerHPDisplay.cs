using UnityEngine;
using UnityEngine.UI;
public class PlayerHPDisplay : MonoBehaviour
{

    [SerializeField] BarUIDisplay hpDisplay;
    [SerializeField] PlayerStatus player;
   
    public void Start()
    {
        hpDisplay.status.damageData.damageHP = hpDisplay.damage;
        hpDisplay.status = player.status;
       
        hpDisplay.status.OnDamage += DelayDamage;
        hpDisplay.status.OnRecover += DelayRecover;
    }
    // Update is called once per frame
    void Update()
    {
        hpDisplay.main.fillAmount =(float) hpDisplay.status.currentHP/hpDisplay.status.maxHP;
    }
  
    public void DelayDamage(DamageData data)
    {
       float ratio= (float)hpDisplay.status.currentHP / hpDisplay.status.maxHP;

        hpDisplay.SetDamage(ratio);
    }
    public void DelayRecover() {

        float ratio = (float)hpDisplay.status.currentHP / hpDisplay.status.maxHP;

        hpDisplay.SetRecover(ratio);
    }
}
