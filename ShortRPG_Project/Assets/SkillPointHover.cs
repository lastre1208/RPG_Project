using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SkillButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    public int index;
    public SkillTooltipManager tooltipManager;
  
   
    public void OnPointerEnter(PointerEventData eventData)
    {
     
            tooltipManager.OnEnter(index);
        


    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
            tooltipManager.OnExit(index);
        
    }
}
