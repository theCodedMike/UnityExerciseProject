using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InputFieldTest : MonoBehaviour
{
    [Header("输入框")]
    public TMP_InputField inputField;


    private void OnEnable()
    {
        inputField.onValueChanged.AddListener(OnValueChanged);
        inputField.onEndEdit.AddListener(OnEndEdit);
        inputField.onSelect.AddListener(OnSelect);
        inputField.onDeselect.AddListener(OnDeselect);
    }

    private void OnDisable()
    {
        inputField.onValueChanged.RemoveAllListeners();
        inputField.onEndEdit.RemoveAllListeners();
        inputField.onSelect.RemoveAllListeners();
        inputField.onDeselect.RemoveAllListeners();
    }

    private void OnDeselect(string value)
    {
        print($"InputField.OnDeselect: {value}");
    }

    private void OnSelect(string value)
    {
        print($"InputField.OnSelect: {value}");
    }

    // 按下回车
    private void OnEndEdit(string value)
    {
        print($"InputField.OnEndEdit: {value}");
    }

    // 输入中...
    void OnValueChanged(string value)
    {
        print($"InputField.OnValueChanged: {value}");
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
