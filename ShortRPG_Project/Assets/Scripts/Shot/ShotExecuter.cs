using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ShotExecuter : MonoBehaviour
{

    [SerializeField] Image shotGauge;
    [SerializeField] Image shotTime;
    [SerializeField]MoveScope moveScope;
    [SerializeField] GameObject shotText;
    [SerializeField] float shotWaitTime;
    [SerializeField] float showShotText;
    float countEnableTime;
    float countShotTime;
    public void Start()
    {
        
        shotText.SetActive(false);
        shotGauge.gameObject.SetActive(false);
    }



    public IEnumerator StartShot(PlayerStatus player )
    {
     
        yield return new WaitForSeconds(shotWaitTime);

        float interval = player.equippedWeapon.fireInterval * player.intervalRatio;
       
        shotText.SetActive(true);
        shotGauge.gameObject.SetActive(true);
        while (countEnableTime < player.shotTime)
        {



            shotTime.fillAmount =1- countEnableTime / player.shotTime;
            shotGauge.fillAmount = countShotTime /interval;
           
            if (countShotTime > interval)
            {
                
                countShotTime = 0f;
                Debug.Log("Shot!");
                moveScope.SwitchScopeCollider(true);

                if (player.status.StateCheck(player.status,EnableState.Panic))
                {
                    var rand = Random.Range(0.5f,2f);
                    interval*=rand;
                }
            }
            if(countEnableTime> showShotText)
            {
                shotText.SetActive(false);

            }


            countEnableTime += Time.deltaTime;
            countShotTime += Time.deltaTime;

             yield return null;

        }
        shotGauge.fillAmount = 0;
        countEnableTime = 0;
        
        shotGauge.gameObject.SetActive(false);
        yield return null;
    }
}
