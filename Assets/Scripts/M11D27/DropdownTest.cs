using TMPro;
using UnityEngine;

public class DropdownTest : MonoBehaviour
{
    [Header("下拉选项")]
    public TMP_Dropdown dropdown;

    private void OnEnable()
    {
        dropdown.onValueChanged.AddListener(OnValueChanged);
    }
    private void OnDisable()
    {
        dropdown.onValueChanged.RemoveAllListeners();
    }

    private void OnValueChanged(int idx)
    {
        print($"Dropdown.OnValueChanged: {idx}, {dropdown.captionText.text}, {dropdown.options[idx].text}");
        // output:
        // 0, Option A, Option A
        // 1, Option B, Option B
        // 2, Option C, Option C
    }
}
