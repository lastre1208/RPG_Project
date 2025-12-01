using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BarUIDisplay : MonoBehaviour
{
    [SerializeField] public Image main;
    [SerializeField] public Image damage;
    [SerializeField] public Image recover;
    [SerializeField] float delayTime = 0.5f;
    [SerializeField] public CommonStatus status;
    [SerializeField] DelayBar delay;
    Coroutine damageCoroutine;
    Coroutine recoverCoroutine_Main;
    Coroutine recoverCoroutine_Damage;


    public void SetDamage(float ratio)//ダメージ用
    {
        main.fillAmount =recover.fillAmount= ratio;

        if (damageCoroutine != null)
            StopCoroutine(damageCoroutine);

        damageCoroutine = StartCoroutine(
            delay.DamageHPCoroutine(main, damage, delayTime)
        );
    }
    public void SetRecover(float ratio)//回復用
    {
        recover.fillAmount = damage.fillAmount=ratio;

        if (recoverCoroutine_Main != null && recoverCoroutine_Damage != null)
        {
            StopCoroutine(recoverCoroutine_Main);
            StopCoroutine(recoverCoroutine_Damage);
        }
           

        recoverCoroutine_Main = StartCoroutine(
            delay.DamageHPCoroutine(recover, main, delayTime)
        );
        recoverCoroutine_Damage= StartCoroutine( 
            delay.DamageHPCoroutine(recover, damage, delayTime)
        );
    }
}
