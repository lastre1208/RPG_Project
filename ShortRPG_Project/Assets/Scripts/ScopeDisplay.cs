using UnityEngine;
using UnityEngine.UI;
public class ScopeDisplay : MonoBehaviour//Æ€‚Ì‘å‚«‚³‚ğ’e‚Ì‘å‚«‚³‚É‡‚í‚¹‚Ä•Ï‚¦‚é
{
    [SerializeField]PlayerStatus player;
    [SerializeField] Image scope;
    [SerializeField]Collider2D scopeCollider;


    public void Update()
    {
        if (scope.enabled == false) return;
       
        float scopeScale = player.equippedWeapon.bulletScale * player.bulletScale;
        scope.transform.localScale = new Vector3(scopeScale, scopeScale, 1f);
        scopeCollider.transform.localScale = new Vector3(scopeScale, scopeScale, 1f);
    }

}
