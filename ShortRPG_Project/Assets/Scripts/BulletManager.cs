using UnityEngine;

public class BulletManager : MonoBehaviour//’e‚Ì‘I‘ğ‚âØ‚è‘Ö‚¦‚Ì§Œä
{
    [SerializeField] PlayerStatus player;
    [SerializeField]MoveScope moveScope;

    public void ChangeWeapon(Weapon change)
    {
        player.equippedWeapon = change;
        moveScope.ChangeScopeCollider(change);
    }
}
