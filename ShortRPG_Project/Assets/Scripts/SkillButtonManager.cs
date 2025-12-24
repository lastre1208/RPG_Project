using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using static CustomManager;
using System.Collections;

public class StatusButtonManager : MonoBehaviour,IPointerDownHandler, IPointerUpHandler,IPointerExitHandler
{
    [SerializeField] CustomManager manager;
    [SerializeField] CustomType type;
    [SerializeField] bool isIncrease;

    private Button button;

    private Coroutine pointCoroutine;
    public CustomType Type { get { return type; } }

    public void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData data)
    {
      pointCoroutine=  StartCoroutine(RepeatCoroutine());
    }

    public void OnPointerUp(PointerEventData data)
    {
        StopRepeat();
    }
    
    public void OnPointerExit(PointerEventData data)
    {

        StopRepeat();
    }
    public IEnumerator RepeatCoroutine()
    {
        yield return new WaitForSecondsRealtime(manager.InitialDelay);

        while (true&&button.interactable)
        {
       yield return     new WaitForSecondsRealtime(manager.RepeatRate);

            if(isIncrease)
            {
                IncreasePoint();
            }
            else
            {
                DecreasePoint();
            }
            manager.customAudioSource.PlayOneShot(manager.customClip);

        }
  
    }
    public void StopRepeat()
    {
        if (pointCoroutine != null)
        {
            StopCoroutine(pointCoroutine);
            pointCoroutine = null;
        }
    }
    public void IncreasePoint()
    {
        if (manager.player.skillPoints <= 0) return;

        manager.customPoints[type] += 1;

        manager.player.skillPoints--;
    }
    public void DecreasePoint()
    {

        if (manager.customPoints[type] <= 0) return;
        manager.customPoints[type] -= 1;

        manager.player.skillPoints++;
    }
}
