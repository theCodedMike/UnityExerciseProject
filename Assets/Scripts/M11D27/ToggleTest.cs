using UnityEngine;
using UnityEngine.UI;

public class ToggleTest : MonoBehaviour
{
    public Toggle toggle;


    void OnEnable()
    {
        toggle.onValueChanged.AddListener(OnValueChanged);
    }
    void OnDisable()
    {
        toggle.onValueChanged.RemoveAllListeners();
    }

    private void OnValueChanged(bool value)
    {
        print($"Toggle.OnValueChanged: {value}");
        // output:
        // False / True
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
