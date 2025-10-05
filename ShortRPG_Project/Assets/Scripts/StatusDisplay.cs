using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StatusDisplay : MonoBehaviour
{
   public  GameObject character;
    public TMP_Text nameText;
    public TMP_Text hpText;
    public TMP_Text mpText;
    PlayerStatus status;
    void Start()
    {
        status = character.GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        nameText.text = status.status.characterName;
        hpText.text = (status.status.currentHP + "/" + status.status.maxHP);
        mpText.text = (status.status.currentMP + "/" + status.status.maxMP);
    }
}
