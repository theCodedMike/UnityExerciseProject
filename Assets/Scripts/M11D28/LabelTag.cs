using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace M11D28
{
    public class LabelTag : MonoBehaviour, IPointerClickHandler
    {
        private Image image;

        private TMP_Text text;

        public Action<LabelTag> OnLabelTagChangeEvent;


        // Start is called before the first frame update
        void Awake()
        {
            image = GetComponent<Image>();
            text = GetComponentInChildren<TMP_Text>();
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            OnLabelTagChangeEvent(this);
        }

        public void ResetColor()
        {
            image.color = new Color32(33, 48, 74, 0);
            text.color = new Color32(91, 117, 153, 255);
        }

        public void SetSelectedColor()
        {
            image.color = new Color32(33, 48, 74, 255);
            text.color = Color.white;
        }

        public string GetText() => text.text;
    }
}
