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

    private void OnValueChanged(int value)
    {
        print($"Dropdown.OnValueChanged: {value}");

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
