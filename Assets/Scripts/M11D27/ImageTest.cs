using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace M11D27
{
    public class ImageTest : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler, IPointerDownHandler, IPointerClickHandler, IPointerExitHandler
    {
        // 不需要鼠标左键按下
        public void OnPointerEnter(PointerEventData eventData)
        {
            print("Image.OnPointerEnter...");
            image.color = Color.red;
        }
        // 不需要鼠标左键按下
        public void OnPointerMove(PointerEventData eventData)
        {
            print("Image.OnPointerMove...");
        }
        // 需要鼠标左键按下，先执行它
        public void OnPointerDown(PointerEventData eventData)
        {
            print("Image.OnPointerDown...");
            image.color = Color.green;
        }
        // 需要鼠标左键按下，再执行它
        public void OnPointerClick(PointerEventData eventData)
        {
            print("Image.OnPointerClick...");
            image.color = Color.blue;
        }
        // 不需要鼠标左键按下，鼠标左键已抬起
        public void OnPointerExit(PointerEventData eventData)
        {
            print("Image.OnPointerExit...");
            image.color = Color.yellow;
        }


        private Image image;
        // Start is called before the first frame update
        void Start()
        {
            image = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
