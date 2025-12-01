using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Profiling.Memory.Experimental;
using Unity.VisualScripting;
using System.Collections;
public class EnemyDisplay : MonoBehaviour
{

    [SerializeField] TMP_Text nameText;
    [SerializeField] GameObject damageText;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BarUIDisplay barDisplay;
    //[SerializeField] Image enemyHP;
    [SerializeField] Image enemyHP_Back;
    //[SerializeField] Image enemyHP_Damage;
    [SerializeField] int enemyHP_diff;
    [SerializeField] Canvas canvas;
    [SerializeField] float damageBlinkTime = 0.05f;
    //[SerializeField] float derayDamageTime = 0.5f;

    DelayBar delay;
    Coroutine damageCoroutine;
    Enemy enemy;
    float countTime = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = this.GetComponent<Enemy>();
        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
        nameText.text = enemy.status.status.characterName;
        delay = this.GetComponent<DelayBar>();
        barDisplay.status = enemy.commonStatus;

         enemy.commonStatus.OnDamage += DrawDamage;
       
        enemy.commonStatus.OnDamage += DelayDamage;
        enemy.commonStatus.OnRecover += DelayRecover;
        enemy.commonStatus.damageData.damageHP = barDisplay.damage;
        Vector3 HPPos = Camera.main.WorldToScreenPoint(enemy.transform.position);
        HPPos.y += enemyHP_diff;

        enemyHP_Back.transform.position = HPPos;

    }

    // Update is called once per frame
    void Update()
    {
        bool isDead = enemy.commonStatus.IsDead();

        nameText.enabled = !isDead;
        enemyHP_Back.gameObject.SetActive(!isDead);


        barDisplay.main.fillAmount = (float)enemy.commonStatus.currentHP / enemy.commonStatus.maxHP;

        if (enemy.IsDamage && !isDead)
        {
            BlinkUpdate();
        }
        else
        {
            spriteRenderer.enabled = !isDead;
        }

    }
    public void DelayRecover()
    {

        float ratio = (float)barDisplay.status.currentHP / barDisplay.status.maxHP;

        barDisplay.SetRecover(ratio);
    }

    public void DelayDamage(DamageData data)
    {
        float ratio = (float)barDisplay.status.currentHP / barDisplay.status.maxHP;

        barDisplay.SetDamage(ratio);
    }
    public void BlinkUpdate()
    {
        countTime += Time.deltaTime;

        if (countTime > damageBlinkTime)
        {
            countTime = 0f;
            spriteRenderer.enabled = !spriteRenderer.enabled;

        }

    }

    public void DrawDamage(DamageData data)
    {
        var obj = Instantiate(damageText);

        obj.transform.SetParent(canvas.transform);

        obj.transform.position = Camera.main.WorldToScreenPoint(data.hitPosition);

        var text = obj.GetComponent<TMP_Text>();

        text.text = data.damage.ToString();

    }
}
