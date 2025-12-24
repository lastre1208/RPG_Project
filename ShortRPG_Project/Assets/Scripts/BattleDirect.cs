using Mono.Cecil;
using System.Collections;
using UnityEngine;

public class BattleProductManager : MonoBehaviour//ƒoƒgƒ‹‚Ì‰‰o“Š‡
{
    [SerializeField]float slowSpeed = 1.0f;
    [SerializeField]float stopTime = 1.0f;
    [SerializeField, Range(0, 1f)] float slowLimit=0;
    public AudioClip defeatSE;
    public AudioSource audioSource;
    public IEnumerator DefeatEnemy()
    {
        var elapsed = 0f;
        audioSource.PlayOneShot(defeatSE);
        while (elapsed<slowSpeed)
        {
            elapsed += Time.unscaledDeltaTime;
            var t=elapsed/slowSpeed;

            Time.timeScale=Mathf.Lerp(1f, slowLimit, t);


            yield return null;
        }

        Time.timeScale = slowLimit;

        new WaitForSecondsRealtime(stopTime);

        Time.timeScale = 1f;
    }

}
