using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace M11D28
{
    public class ItemView : MonoBehaviour, IPointerClickHandler
    {
        [Header("图标")]
        public Image image;
        [Header("左下角数字")]
        public TMP_Text text;

        public Item item;

        [HideInInspector]
        public Sprite sprite;

        public Action<ItemView> OnSelectedItemView;

        private Image bgImage; // 物体的背景板，是一张图片

        void Awake()
        {
            bgImage = transform.GetComponent<Image>();
        }
        public void SetValue(Item item)
        {
            this.item = item;
            Refresh();
        }

        public void Refresh()
        {
            image.sprite = Resources.Load<Sprite>($"UI/Bag/{this.item.name}");
            this.sprite = image.sprite;
            text.text = $"{item.count}";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnSelectedItemView(this);
        }

        // Item被选中后，改变其背景图片
        public void ChangeBackgroundWhenClick()
        {
            bgImage.sprite = Resources.Load<Sprite>("UI/Bg/frame_char");
        }

        // Item失去焦点后恢复背景
        public void RestoreBackgroundWhenClick()
        {
            bgImage.sprite = null;
            bgImage.color = new Color32(169, 173, 133, 255);
        }
    }
}
