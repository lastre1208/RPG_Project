using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class JumpText : MonoBehaviour
{
    [SerializeField] AnimationCurve jumpCurve;
    [SerializeField] float jumpHigh=100f;
    [SerializeField] float jumpDuration = 0.5f;
    [SerializeField]float randomXMove=100f;
    RectTransform jumpRectTransform;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpRectTransform = GetComponent<RectTransform>();

        StartCoroutine(JumpCoroutine());
    }


    IEnumerator JumpCoroutine()
    {
        Vector3 start = jumpRectTransform.position;
        float target_Y = start.y + jumpHigh;
        float target_X = start.x + Random.Range(-randomXMove, randomXMove);



        float elapsed = 0f;
        while (elapsed < jumpDuration)
        {


            elapsed += Time.deltaTime;

            var t = elapsed / jumpDuration;
            float curve = jumpCurve.Evaluate(t);
            float y = Mathf.Lerp(start.y, target_Y, t) + Mathf.Sin(t * Mathf.PI) * jumpHigh * curve;

            float x = Mathf.Lerp(start.x, target_X, t);

            jumpRectTransform.position = new Vector3(x, y, 0);


            yield return null;
        }

        jumpRectTransform.position = new Vector3(target_X, target_Y, 0);

    }
}
