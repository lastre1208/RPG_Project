using UnityEngine;
using UnityEngine.UI;
public class MoveScope : MonoBehaviour//�}�E�X�̈ʒu�ɏƏ����ړ�   
{
    [SerializeField]Image scope;//�W���̉摜
    [SerializeField] GameObject shotEffect;
    [SerializeField]Collider2D scopeCollider;//�W���̃R���C�_�[
    [SerializeField]float enableTime;//�R���C�_�[�̗L������
    private Vector3 mousePos;
    float countTime;
    private void Start()
    {
        scopeCollider.enabled=false;
    }
    public void SwitchScopeCollider(bool scopeSwitch)//�R���C�_�[��L���ɂ���
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

    public Vector3 ReturnWorldScopePos()//�}�E�X�̃��[���h���W��Ԃ�
    {


        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
