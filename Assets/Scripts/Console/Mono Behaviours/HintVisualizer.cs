using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintVisualizer : MonoBehaviour
{
    public TextMeshProUGUI hintText;

    public void SetHintText(string text)
    {
        hintText.text = text;
    }
}
