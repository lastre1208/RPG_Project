using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;
[System.Serializable]

public class EncountPattern
{
    public List<EncountData> EncountList; 

}

[System.Serializable]

public class EncountData
{
  public List<EnemyStatus> EnemyList;
    public List<Vector2> EnemyPositionList;
}
public class EncountManager : MonoBehaviour
{
    public GameObject enemy;
    public List<EncountPattern> encountPatterns;


    public List<Enemy> EncountEnemy(int level)
    {
       var rand = Random.Range(0, encountPatterns[level].EncountList.Count);
        List<EnemyStatus> enemyStatusList = encountPatterns[level].EncountList[rand].EnemyList;
        List<Vector2> positionList = encountPatterns[level].EncountList[rand].EnemyPositionList;
       
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
