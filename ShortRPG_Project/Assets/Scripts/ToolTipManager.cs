using UnityEngine;


public class SkillTooltipManager : MonoBehaviour
{
    [SerializeField] SkillDisplay skill;
    [SerializeField] float enterDelay = 0.2f;
    [SerializeField] float exitDelay = 0.2f;

    int currentIndex = -1;      // 今カーソルが乗っているボタン
    int shownIndex = -1;      // 今説明を出しているボタン

    float enterTimer = 0f;
    float exitTimer = 0f;
    bool isHovering = false;   // どこかのボタンに乗っているか
    bool isVisible = false;   // 説明テキストが出ているか

    void Update()
    {
        if (isHovering)
        {
            enterTimer += Time.deltaTime;

            // まだ何も出ていない or 「別のボタンに差し替えたい」状態
            if (!isVisible && enterTimer >= enterDelay)
            {
                skill.ShowDiscription(currentIndex);
                shownIndex = currentIndex;
                isVisible = true;
                exitTimer = 0f;
            }
        }
        else
        {
            enterTimer = 0f;

            if (isVisible)
            {
                exitTimer += Time.deltaTime;
                if (exitTimer >= exitDelay)
                {
                    skill.RemoveDiscription();
                    isVisible = false;
                    shownIndex = -1;
                }
            }
            else
            {
                exitTimer = 0f;
            }
        }
    }

    public void OnEnter(int index)
    {
        isHovering = true;
        currentIndex = index;
        enterTimer = 0f;

        // ★すでに何か表示されていて、別のボタンに移った場合は「差し替えモード」にする
        if (isVisible && shownIndex != currentIndex)
        {
            // ここでは一旦「非表示扱い」にして、enterDelay 後に currentIndex で表示し直す
            isVisible = false;
            // RemoveDiscription はここでは呼ばない（exitDelay を待たずに即消すかどうかは好み）
            // 今の説明を即消したいなら skill.RemoveDiscription(); もここで呼ぶ
            exitTimer = 0f;
        }
    }
   

    public void OnExit(int index)
    {
        // 完全に外れたかどうかは分からないので、ここでは「hoverしていない状態」にしておく
        isHovering = false;
        enterTimer = 0f;
    }
}


