using UnityEngine;
using UnityEngine.UI;

public class ToggleGroupTest : MonoBehaviour
{
    [Header("选项集")]
    public Toggle[] toggles;

    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var toggle in toggles) {
            toggle.onValueChanged.AddListener(isOn =>
            {
                if (isOn) {
                    print("现在选中的是 " + toggle.GetComponentInChildren<Text>().text);
                }
            });
        }

        // output: 
        // 现在选中的是 篮球/足球/乒乓球
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
