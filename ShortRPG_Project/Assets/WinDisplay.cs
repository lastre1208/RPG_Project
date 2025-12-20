using System.Collections;
using System.Xml.Xsl;
using TMPro;
using UnityEngine;

public class WinDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text winText;
    [SerializeField] float targePos;
    [SerializeField] float moveSpeed;

    Vector2 defaultPos;
   
    void Start()
    {
    var  rect = winText.GetComponent<RectTransform>();
        defaultPos = rect.anchoredPosition;   // 初期位置を保存
    }

    public IEnumerator WinCoroutine()
    {
        RectTransform rect = winText.GetComponent<RectTransform>();

       
        rect.anchoredPosition = defaultPos;
        Vector3 pos = rect.anchoredPosition;

        // 目標位置に近づくまで移動
        while (Mathf.Abs(pos.x - targePos) > 0.01f)
        {
            pos = rect.anchoredPosition;
            float dir = Mathf.Sign(targePos - pos.x); // 進行方向
            float delta = moveSpeed * Time.deltaTime * dir;

            // 行き過ぎ防止
            if (Mathf.Abs(delta) > Mathf.Abs(targePos - pos.x))
                delta = targePos - pos.x;

            rect.anchoredPosition = new Vector2(pos.x + delta, pos.y);

            yield return null;
        }

        // 最終位置をきっちり揃える
        rect.anchoredPosition = new Vector2(targePos, rect.anchoredPosition.y);
    }
}


