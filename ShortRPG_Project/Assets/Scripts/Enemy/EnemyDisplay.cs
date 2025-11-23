using UnityEngine;
using TMPro;
using UnityEditor.Profiling.Memory.Experimental;
public class EnemyDisplay : MonoBehaviour
{

    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text damageText;
    [SerializeField]SpriteRenderer spriteRenderer;
    [SerializeField]float damageDisplayTime = 1f;
    Enemy enemy;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = this.GetComponent<Enemy>();

        nameText.text = enemy.status.status.characterName;

        enemy.OnDamaged += DrawDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.IsDamage)
        {
            if (enemy.CountTime >=damageDisplayTime)
            {
                damageText.text = "";      
                
            }
        }
    }

    public void DrawDamage(int damage)
    {
        damageText.text = damage.ToString();

    }
}
