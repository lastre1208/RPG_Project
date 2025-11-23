using UnityEngine;
using UnityEngine.UI;
public class MoveScope : MonoBehaviour//マウスの位置に照準を移動   
{
   
    [SerializeField]Image scope;//照準の画像
    [SerializeField] GameObject shotEffect;
    [SerializeField]Collider2D scopeCollider;//標準のコライダー
    [SerializeField]float enableTime;//コライダーの有効時間
    private Vector3 mousePos;
  
    float countTime;
    private void Start()
    {
        scopeCollider.enabled=false;
      
    }
    public void SwitchScopeCollider(bool scopeSwitch)//コライダーを有効にする
    {
        if (scopeSwitch) {
            Instantiate(shotEffect, ReturnWorldScopePos(), Quaternion.identity);
        }
        scopeCollider.enabled=scopeSwitch;
        scopeCollider.transform.position = ReturnWorldScopePos();
       
    }


    void Update()
    {
        if (scope.enabled == false) return;

        mousePos = Input.mousePosition;

 

        scope.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        if (scopeCollider.enabled == true)
        {
            countTime+= Time.deltaTime;
            if (enableTime <countTime)
            {
                scopeCollider.enabled = false;
                countTime = 0f;
               
            }
        }

    }

    public Vector3 ReturnWorldScopePos()//マウスのワールド座標を返す
    {


        return Camera.main.ScreenToWorldPoint(mousePos);
    }

  
}
