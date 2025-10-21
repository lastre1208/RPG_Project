using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ShotExecuter : MonoBehaviour
{
    [SerializeField] GameObject shotIcon;
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
        shotIcon.SetActive(false);
        shotText.SetActive(false);
        shotGauge.gameObject.SetActive(false);
    }



    public IEnumerator StartShot(PlayerStatus player )
    {
         shotIcon.SetActive(true);
        yield return new WaitForSeconds(shotWaitTime);

       
        shotText.SetActive(true);
        shotGauge.gameObject.SetActive(true);
        while (countEnableTime < player.shotTime)
        {
            shotTime.fillAmount =1- countEnableTime / player.shotTime;
            shotGauge.fillAmount = countShotTime / player.equippedWeapon.fireInterval;
            if (countShotTime > player.equippedWeapon.fireInterval)
            {
                
                countShotTime = 0f;
                Debug.Log("Shot!");
                moveScope.SwitchScopeCollider(true);
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
        shotIcon.SetActive(false);
        shotGauge.gameObject.SetActive(false);
        yield return null;
    }
}
