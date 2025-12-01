using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class BulletDisplay : MonoBehaviour
{
    [SerializeField] List<Image> bulletIcons;
    [SerializeField] List<Image> BackImages;
    [SerializeField]List<Weapon> weapon;
    [SerializeField] PlayerStatus player;
    [SerializeField] Color selectedColor;
    [SerializeField] Color defaultColor;
    [SerializeField] TMP_Text equipText;
    void Start()
    {
        weapon = player.havedWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < bulletIcons.Count; i++)
        {
            if (weapon[i] == player.equippedWeapon)
            {

               BackImages[i].color = selectedColor;
            }
            else
            {
                BackImages[i].color=defaultColor;
            }
            bulletIcons[i].sprite = weapon[i].icon;
        }
      
    }

    public void LeftText()
    {
        equipText.alignment = TextAlignmentOptions.Left;
    }

    public  void RightText()
    {
        equipText.alignment = TextAlignmentOptions.Right;

    }
    public void CenterText()
    {
        equipText.alignment = TextAlignmentOptions.Center;
    }
}
