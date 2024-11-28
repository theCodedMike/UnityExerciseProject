using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.M11D28
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
    }
}
