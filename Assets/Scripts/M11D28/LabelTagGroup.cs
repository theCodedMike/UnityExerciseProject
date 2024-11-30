using UnityEngine;

namespace Assets.Scripts.M11D28
{
    public class LabelTagGroup : MonoBehaviour
    {
        private LabelTag lastSelectedTag;

        private LabelTag currentSelectedTag;

        private LabelTag[] labelTags;

        private ItemViewGroup itemViewGroup;

        // Start is called before the first frame update
        void Start()
        {
            labelTags = transform.GetComponentsInChildren<LabelTag>();
            foreach (var labelTag in labelTags)
            {
                labelTag.OnLabelTagChangeEvent = ChangeLabelState;
            }

            currentSelectedTag = labelTags[0];
            lastSelectedTag = currentSelectedTag;
            currentSelectedTag.SetSelectedColor();

            itemViewGroup = transform.root.GetComponentInChildren<ItemViewGroup>();
        }

        public void ChangeLabelState(LabelTag current)
        {
            if (current == currentSelectedTag)
                return;
            
            lastSelectedTag.ResetColor();
            currentSelectedTag = current;
            currentSelectedTag.SetSelectedColor();
            lastSelectedTag = currentSelectedTag;


            itemViewGroup.OnLabelTagChange(current);
        }
    }
}
