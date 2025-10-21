using UnityEngine;
[CreateAssetMenu(fileName = "EnemyStatus", menuName = "ScriptableObjects/EnemyStatus")]

[System.Serializable]
public class EnemyStatus : ScriptableObject
{

    public enum HowIntelligent
    {
        Fool,
        Normal,
        Smart
    }

   public CommonStatus status;
    public int ID;
    public int exp;
   
}
