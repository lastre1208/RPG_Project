using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;
using JetBrains.Annotations;


[System.Serializable]

public class EncountPattern
{
    public List<EncountData> EncountList; 

}

[System.Serializable]

public class EncountData
{
    public CustomRank rank;
  public List<EnemyStatus> EnemyList;
    public List<Vector2> EnemyPositionList;
}
public class EncountManager : MonoBehaviour
{
    public GameObject enemy;
    public List<EncountPattern> encountPatterns;


    public List<Enemy> EncountEnemy(int level,CustomRank rank)//指定されたレベルとランクのパターンからランダムに選出する
    {
        List <EncountData>appliedPatterns=new List<EncountData>();
        foreach (EncountData pattern in encountPatterns[level].EncountList) {

            if (pattern.rank == rank) {
                appliedPatterns.Add(pattern);

            }
        } 
       var rand = Random.Range(0, appliedPatterns.Count);
        List<EnemyStatus> enemyStatusList = appliedPatterns[rand].EnemyList;
        List<Vector2> positionList = appliedPatterns[rand].EnemyPositionList;
       
        int listCount = enemyStatusList.Count;

        if (listCount != positionList.Count) return null;

        var spawnedEnemy=new List<Enemy>();

        for(int i = 0; i < listCount; i++)

        {
           

          var obj=  Instantiate(enemy, positionList[i],Quaternion.identity);

            var enemyData=obj.GetComponent<Enemy>();

            enemyData.Init(enemyStatusList[i]);

            spawnedEnemy.Add(enemyData);
            
        }
       
        return spawnedEnemy;
    }
    
}
