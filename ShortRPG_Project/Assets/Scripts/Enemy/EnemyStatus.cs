using JetBrains.Annotations;
using UnityEngine;



    public enum Intelligence
    {
        Fool,
        Normal,
        Smart
    }

[System.Serializable]
[CreateAssetMenu(fileName = "EnemyStatus", menuName = "ScriptableObjects/EnemyStatus")]
public class EnemyStatus : ScriptableObject
{
   
  
   public CommonStatus status;
    public int ID;
    public int exp;
    public float speed;
    public Intelligence intelligence;
    public float takedamageRatio=1.0f;//与えるダメージ倍率
    [Range(0.0f,1.0f)]public float decayDamageRatioLimit=1.0f;//最大減衰率
    public float easydecayDamage=100f;//減衰のしやすさ(どれくらいのダメージをウケたら最大減衰率に達するか)
    [Range(0.0f,1.0f)]public float recoverDamageRatio=0.1f;//ダメージ倍率回復量(%回復)
}
