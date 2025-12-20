using UnityEngine;
using UnityEngine.UI;
using static CustomManager;

public class SkillButtonManager : MonoBehaviour
{
    [SerializeField] CustomManager manager;
    [SerializeField] CustomType type;

  

  
    public CustomType Type { get { return type; } }
    public void IncreasePoint()
    {

        manager.customPoints[type] += 1;

        manager.player.skillPoints--;
    }
    public void DecreasePoint()
    {
        manager.customPoints[type] -= 1;

        manager.player.skillPoints++;
    }
}
