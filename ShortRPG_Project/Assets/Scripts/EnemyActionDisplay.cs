using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EnemyActionDisplay : MonoBehaviour//“G‚ª‚Ç‚Ìs“®‚ğ‚·‚é‚Ì‚©‚ğ¦‚·
{
    [SerializeField] Image actionImage;
    [SerializeField]TMP_Text numberText;
    [SerializeField] SkillImageDatabase database;

    Enemy enemy;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     enemy = GetComponent<Enemy>();   
    }

    // Update is called once per frame
    void Update()
    {
        actionImage.sprite = database.SetSkillImage(enemy.nextSkill);
        numberText.text = database.SetSkillNum(enemy.nextSkill, enemy);
    }
}
