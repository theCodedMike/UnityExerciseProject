using System;
using TMPro;
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
    }

    private void OnSelect(string value)
    {
        print($"InputField.OnSelect: {value}");
    }

    private void OnEndEdit(string value)
    {
        print($"InputField.OnEndEdit: {value}");
    }

    // 按下回车
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
