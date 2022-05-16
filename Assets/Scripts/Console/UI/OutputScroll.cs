using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameConsole.UI
{
    public class OutputScroll : MonoBehaviour, IDragHandler, IBeginDragHandler
    {
        public  Action<float> onValueChanged;

        private Vector2 lastPos;

        public float CumulativeChange { get; private set; }

        public void OnBeginDrag(PointerEventData eventData)
        {
            lastPos = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            float delta = lastPos.y - eventData.position.y;
            CumulativeChange += delta;
            lastPos = eventData.position;
            onValueChanged?.Invoke(CumulativeChange);
        }

        public void Reset()
        {
            CumulativeChange = 0;
        }
    }

}