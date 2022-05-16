using TMPro;
using UnityEngine;
using UnityEngine.Events;

[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class TextFitter : MonoBehaviour
{
    public UnityEvent onRectTransformDimensionsChange;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void UpdateText(TMP_TextInfo textInfo)
    {
        float height;

        if (textInfo.lineCount <= 0)
        {
            height = 0;
        }
        else
        {
            height = textInfo.lineCount * textInfo.lineInfo[0].lineHeight;
        }

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        onRectTransformDimensionsChange?.Invoke();
    }
}
