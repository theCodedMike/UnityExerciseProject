using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderTest : MonoBehaviour
{
    public Slider slider;


    void OnEnable()
    {
        slider.onValueChanged.AddListener(OnValueChanged);
    }
    void OnDisable()
    {
        slider.onValueChanged.RemoveAllListeners();
    }

    // 范围[0~1] 
    private void OnValueChanged(float value)
    {
        print($"Slider.OnValueChanged: {value}");
        // output:
        //  0.1478107
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
