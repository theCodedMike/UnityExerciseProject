using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace M12D06
{
    public class UIDisplay : MonoBehaviour
    {
        public Slider slider;

        public TMP_Text text;


        // Start is called before the first frame update
        void Start()
        {
            Loading.OnDisplayProgress = Display;
        }

        private void Display(int progress)
        {
            slider.value = progress;
            text.text = $"Loading...{progress}";
        }
    }
}
