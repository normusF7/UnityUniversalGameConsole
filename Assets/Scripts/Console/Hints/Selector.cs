using TMPro;
using UnityEngine;

namespace GameConsole.Hints
{
    public class Selector
    {
        private readonly HintSystem hintSystem;
        private RectTransform selectorRectTransform;
        private TMP_TextInfo textInfo;

        public bool isActive { get; private set; }
        public int currentIndex { get; private set; }

        public Selector(HintSystem hintSystem, RectTransform selectorRectTransform)
        {
            this.hintSystem = hintSystem;
            this.selectorRectTransform = selectorRectTransform;
            SetActive(false);
        }

        public void UpdateSelector(TMP_TextInfo textInfo)
        {
            this.textInfo = textInfo;
        }

        public void SetOnIndex(int index)
        {
            if(!isActive || index < 0 || index >= hintSystem.currentHintResults.Length)
            {
                SetOnFirstAvaibleIndexOrDisable(index);
                return;
            }

            float newPos = -textInfo.lineInfo[0].lineHeight * index;
            selectorRectTransform.anchoredPosition = new Vector2(selectorRectTransform.anchoredPosition.x, newPos);
            currentIndex = index;
        }

        public void SetOnIndex(bool next)
        {
            int newIndex = next ? currentIndex + 1 : currentIndex - 1;
            SetOnIndex(newIndex);
        }

        public void SetOnFirstAvaibleIndexOrDisable(int index)
        {
            if(index >= 0 && hintSystem.currentHintResults != null && hintSystem.currentHintResults.Length > 0)
            {
                if(!isActive)
                {
                    SetActive(true);
                }

                SetOnIndex(0);
            }
            else
            {
                SetActive(false);
            }
        }

        public void SetActive(bool set)
        {
            bool wasActive = isActive;
            isActive = set;
            selectorRectTransform.gameObject.SetActive(set);

            if (!wasActive && set)
            {
                SetOnIndex(0);
            }

            currentIndex = -1;
        }
    }
}
