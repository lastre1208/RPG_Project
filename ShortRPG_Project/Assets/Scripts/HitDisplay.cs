using System.Collections;
using TMPro;
using UnityEngine;

public class HitDisplay : MonoBehaviour
{

    [SerializeField] PlayerStatus player;
    [SerializeField] TMP_Text text;
    [SerializeField] float moveDistance = 500f;
    [SerializeField]float moveDuration = 0.5f;
    [SerializeField]float alphaDration = 0.2f;
   public Vector3 defaultPos;
    RectTransform Rect;
    Color defaultColor;
    float targetAlpha = 1f;
    private void Start()
    {
        Rect = GetComponent<RectTransform>();
        defaultColor =text.color;
      
        defaultPos = Rect.position;
       
        player.hitEvent+= HitEffect;

    }

    // Update is called once per frame
    void Update()
    {
     text.text=player.hitCount.ToString()+"Hit!!!";   
    }


    public void HitEffect()
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(HitCoroutine());
        
    }

    public void StopHit()
    {
        StopCoroutine(HitCoroutine());
    }
    IEnumerator HitCoroutine()
    {
        if (!gameObject.activeInHierarchy) yield break;

        Rect.position=defaultPos;


        text.color=defaultColor;
       
        var elpased = 0f;
        var start_X = Rect.position.x;
        var target_X=start_X+moveDistance;

        var position_X=0f;


        var start_Alpha=0f;
        var alpha=0f;
        var nowColor=text.color;

        while (elpased < moveDuration)
        {
            elpased += Time.deltaTime;

            var t = elpased / moveDuration;
            var t_alpha = elpased / alphaDration;
            var eased_t = 1f - (1f - t) * (1f - t);
            var eased_alpha=1f - (1f - t_alpha) * (1f - t_alpha);
            position_X = Mathf.Lerp(start_X, target_X, eased_t);

            alpha = Mathf.Lerp(start_Alpha, targetAlpha, eased_alpha);



            text.color=new(nowColor.r,nowColor.g,nowColor.b,alpha);

            Rect.position = new(position_X, defaultPos.y,defaultPos.z);



            yield return null;
        }
        text.color=new(defaultColor.r,defaultColor.g,defaultColor.b,targetAlpha);
        Rect.position=new (defaultPos.x+moveDistance,defaultPos.y, defaultPos.z);
    }
}
