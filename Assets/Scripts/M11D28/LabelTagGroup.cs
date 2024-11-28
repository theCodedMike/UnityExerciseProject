using UnityEngine;

namespace Assets.Scripts.M11D28
{
    public class LabelTagGroup : MonoBehaviour
    {
        private LabelTag lastSelectedTag;

        private LabelTag currentSelectedTag;


        // Start is called before the first frame update
        void Start()
        {
            Transform child = transform.GetChild(0);
            lastSelectedTag = child.GetComponent<LabelTag>();
            currentSelectedTag = lastSelectedTag;
            currentSelectedTag.SetSelectedColor();
        }

        public void ChangeLabelState(LabelTag current)
        {
            if (current == currentSelectedTag)
                return;
            
            lastSelectedTag.ResetColor();
            currentSelectedTag = current;
            currentSelectedTag.SetSelectedColor();
            lastSelectedTag = currentSelectedTag;

        }
    }
}
