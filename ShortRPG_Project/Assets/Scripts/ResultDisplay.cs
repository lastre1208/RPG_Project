using TMPro;
using UnityEngine;

public class ResultDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text resultText;
    [SerializeField] TMP_Text resultSumText;
    [SerializeField]BattleManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // manager = GetComponent<BattleManager>();

    }

    public void StartResult()
    {
        resultText.text = "ヒットボーナス:" + manager.player.hitsumCount / manager.hitDevidePoint + "\n"
            + "連続ヒットボーナス:" + manager.player.maxHitCount + "\n"
            + "スピード撃破ボーナス:" + manager.getSkillPoint + "\n"
            + "敵撃破ボーナス:" + manager.enemyGetPoint;

        var sum = (manager.player.hitsumCount / manager.hitDevidePoint)
            + manager.player.maxHitCount
            + manager.getSkillPoint
            + manager.enemyGetPoint;
        resultSumText.text = "獲得ポイント:"+sum.ToString();
    }
}
