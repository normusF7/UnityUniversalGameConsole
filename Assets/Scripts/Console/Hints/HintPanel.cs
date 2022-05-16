using UnityEngine;
using TMPro;

namespace GameConsole.Hints
{
    public class HintPanel : MonoBehaviour
    {
        public RectTransform selectorRect;
        [SerializeField] private TextFitter textFitter;
        [SerializeField] private TextMeshProUGUI hintResultsText;

        private HintSystem hintSystem;

        public void Initialize(HintSystem hintSystem)
        {
            this.hintSystem = hintSystem;
        }

        private void Update()
        {
            ProcessInput();
        }

        public void UpdateHintResultsText(HintResult[] results)
        {
            hintResultsText.text = string.Empty;

            foreach (HintResult result in results)
            {
                if(result.IsValid)
                {
                    hintResultsText.text += string.Format("{0}\n", result);
                }
            }

            UpdateHintPanel(hintResultsText.GetTextInfo(hintResultsText.text));
        }

        public void ShowDetailedDescription(HintResult result)
        {
            //HACK: temp solution 
            if(result.command.method.GetParametersCount() > 0)
                hintResultsText.text = string.Format("{0} {1}", result, result.command.method.parmaterers[0].description);

            UpdateHintPanel(hintResultsText.GetTextInfo(hintResultsText.text));
        }

        internal void ClearHintResults()
        {
            hintResultsText.text = string.Empty;

            UpdateHintPanel(hintResultsText.GetTextInfo(hintResultsText.text));
        }

        private void UpdateHintPanel(TMP_TextInfo textInfo)
        {
            textFitter.UpdateText(textInfo);
            hintSystem.selector.UpdateSelector(textInfo);
        }

        private void ProcessInput()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                hintSystem.selector.SetOnIndex(true);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                hintSystem.selector.SetOnIndex(false);
            }
            else if(Input.GetKeyDown(KeyCode.Tab))
            {
                hintSystem.TryCompleteInput();
            }
        }
    }
}
