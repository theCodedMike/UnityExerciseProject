using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace M11D27
{
    public class TextTest : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler, IPointerDownHandler, IPointerClickHandler, IPointerExitHandler
    {
        private TextMeshProUGUI textMeshPro;
        // Start is called before the first frame update
        void Start()
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update() { }

        public void OnPointerEnter(PointerEventData eventData)
        {
            print("Text.OnPointerEnter...");
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            print("Text.OnPointerMove...");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            print("Text.OnPointerDown...");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            print("Text.OnPointerClick...");
            textMeshPro.text += "!";
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            print("Text.OnPointerExit...");
            textMeshPro.color = Random.ColorHSV();
        }
    }
}
