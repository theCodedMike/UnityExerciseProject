using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace M11D28
{
    public class ItemInfo : MonoBehaviour
    {
        public Image image;
        public TMP_Text nameText;
        public TMP_Text descText;
        public Button useBtn;

        public Action<ItemView> OnUseBtnClickEvent;


        private CanvasGroup canvasGroup;
        private ItemView itemView;

        void OnEnable()
        {
            useBtn.onClick.AddListener(OnUseBtnClick);
        }
        void OnDisable()
        {
            useBtn.onClick.RemoveListener(OnUseBtnClick);
        }
        private void OnUseBtnClick()
        {
            OnUseBtnClickEvent(this.itemView);
        }

        // Start is called before the first frame update
        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            Clear();
        }

        public void SetItemView(ItemView itemView)
        {
            SetUiEffect();
            this.itemView = itemView;
            Refresh();
        }

        void SetUiEffect()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
        }

        void Refresh()
        {
            image.sprite = this.itemView.sprite;
            nameText.text = itemView.item.name;
            descText.text = itemView.item.describe;
        }

        public void Clear()
        {
            image.sprite = null;
            nameText.text = "";
            descText.text = "";
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
        }
    }
}
