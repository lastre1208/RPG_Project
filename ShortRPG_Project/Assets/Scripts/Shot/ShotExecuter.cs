using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShotExecuter : MonoBehaviour
{
    [SerializeField] TMP_Text hitText;
    [SerializeField] TMP_Text countDownText;
    [SerializeField] Image shotGauge;
    [SerializeField] Image shotTime;
    [SerializeField]MoveScope moveScope;
    [SerializeField] TMP_Text shotText;
    [SerializeField]BattleManager battleManager;
    [SerializeField] float shotWaitTime;
    [SerializeField] float showShotText;
    [SerializeField]AudioSource shotAudioSource;
    [SerializeField] AudioClip shotAudioClip;
    float countEnableTime;
    float countShotTime;
    public void Start()
    {
        
        shotText.enabled=(false);
        countDownText.enabled=(false);
        hitText.enabled=(false);
        shotGauge.gameObject.SetActive(false);
    }



    public IEnumerator StartShot(PlayerStatus player )
    {
        shotTime.fillAmount = 1;
        countDownText.enabled=true;
        for(int i = 3; i > 0; i--)
        {
            float wait = shotWaitTime / 3;
            countDownText.text = i.ToString();
            yield return new WaitForSeconds(wait);
        }

       countDownText.enabled = false;
      //  yield return new WaitForSeconds(shotWaitTime);

        float interval = player.equippedWeapon.fireInterval * player.intervalRatio;
       
        shotText.enabled = (true);
        hitText.enabled = (true);
        shotGauge.gameObject.SetActive(true);
        while (countEnableTime < player.shotTime&&battleManager.state==BattleState.PLAYERTURN)
        {



            shotTime.fillAmount =1- countEnableTime / player.shotTime;
            shotGauge.fillAmount = countShotTime /interval;
           
            if (countShotTime > interval)
            {
                
                countShotTime = 0f;
                Debug.Log("Shot!");
                moveScope.SwitchScopeCollider(true);
                shotAudioSource.PlayOneShot(shotAudioClip);
                if (player.status.StateCheck(player.status,EnableState.Panic))
                {
                    var rand = Random.Range(0.5f,2f);
                    interval*=rand;
                }
            }
            if(countEnableTime> showShotText)
            {
                shotText.enabled = (false);
              
            }


            countEnableTime += Time.deltaTime;
            countShotTime += Time.deltaTime;

             yield return null;

        }
        shotTime.fillAmount = 0;
        shotGauge.fillAmount = 0;
        countEnableTime = 0;
        countShotTime = 0;
        hitText.enabled = (false);
        shotGauge.gameObject.SetActive(false);
        yield return null;
    }
}
