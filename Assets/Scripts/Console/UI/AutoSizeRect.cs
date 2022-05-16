using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class AutoSizeRect : MonoBehaviour
{
    public bool fitVertical;
    public bool fitHorizontal;
    public RectTransform rectToFit;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void UpdateContainerRectSize()
    {
        if(!rectToFit || !rectTransform)
        {
            return;
        }

        if (fitVertical)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectToFit.rect.height);
        }
        if (fitHorizontal)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectToFit.rect.width);
        }
    }
}
