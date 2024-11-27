using UnityEngine;

public class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    public CanvasGroup CanvasGroup { get { return canvasGroup ??= GetComponent<CanvasGroup>(); } }

    /// <summary>
    /// 进入当前Panel执行OnEnter
    /// </summary>
    public virtual void OnEnter()
    {
        CanvasGroup.alpha = 1;
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 显示Panel，该Panel上面的所有UI控件不可以交互
    /// </summary>
    public virtual void OnPause()
    {
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// 显示Panel，该Panel上面的所有UI控件可以交互
    /// </summary>
    public virtual void OnResume()
    {
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 退出当前Panel执行OnExit
    /// </summary>
    public virtual void OnExit()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
    }
}
