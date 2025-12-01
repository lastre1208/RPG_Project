using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DelayBar : MonoBehaviour
{
    
    public IEnumerator DamageHPCoroutine(Image main, Image damage, float time)
    {

        var start = damage.fillAmount;
        var end = main.fillAmount;
        var elpased = 0f;
        while (elpased < time)
        {
            elpased += Time.deltaTime;
            var t = elpased / time;

            var eased = 1f - (1f - t) * (1f - t);
            damage.fillAmount = Mathf.Lerp(start, end, eased);

            yield return null;
        }

        damage.fillAmount=main.fillAmount;
    }
}
