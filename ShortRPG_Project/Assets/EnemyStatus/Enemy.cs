using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public EnemyStatus status;
    public TMP_Text nameText;
    public SpriteRenderer characterImage;
    public TMP_Text damageText;
    public int maxHP;
    public int currentHP;
    public int maxMP;
    public int currentMP;
    public int attackPower;
    public int defensePower;
    public int speed;

    bool isDamage;
    float countTime;
    private void Start()
    {
        characterImage=this.GetComponent<SpriteRenderer>();
        characterImage.sprite = status.status.characterImage;
        TMP_Text[]TMP_Texts= this.GetComponentsInChildren<TMP_Text>();

        nameText = TMP_Texts[0];
        nameText.text = status.status.characterName;
        damageText=TMP_Texts[1];

        maxHP = status.status.maxHP;
        currentHP = status.status.currentHP;

        maxMP = status.status.maxMP;
        currentMP = status.status.currentMP;

        attackPower = status.status.attackPower;
        defensePower = status.status.defensePower;
    }

  
    private void Update()
    {
        characterImage.transform.position = this.transform.position;
        if (isDamage)
        {
            countTime += Time.deltaTime;

            if(countTime >= 0.5f)
            {
                damageText.text = "";
                isDamage = false;
                countTime = 0f;
            }
        }
    }
    public void TakeDamage(int damage)
    {

        currentHP -= damage;

        
    }
    public bool IsDead()
    {

        return currentHP <= 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)//UŒ‚‚ðŽó‚¯‚é‚Æ‚«
    {
        if (collision.gameObject.tag == "PlayerAttack")
        {
            countTime = 0;
            Debug.Log("aaa");
            TakeDamage(collision.gameObject.GetComponent<Weapon>().attackPower);
            damageText.text = collision.gameObject.GetComponent<Weapon>().attackPower.ToString();
            isDamage = true;
        }

    }
}
