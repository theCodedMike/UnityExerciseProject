using UnityEngine;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    [Header("测试的按钮2")]
    public Button btn;
    [Header("方块2")]
    public GameObject cube2;
    [Header("小球2")]
    public GameObject sphere2;

    private void OnEnable()
    {
        btn.onClick.AddListener(OnBtnClick);
        //btn.onClick.AddListener(() =>
        //{
        //    print("this is OnEnable()...");
        //});
    }

    private void OnBtnClick()
    {
        print("Button.OnBtnClick...");
        HideCube();
        ChangeSphere();
    }

    private void HideCube()
    {
        cube2.SetActive(false);
    }

    private void ChangeSphere()
    {
        sphere2.transform.localScale = Vector3.one * 2;
        sphere2.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        btn.onClick.RemoveListener(OnBtnClick);
        //btn.onClick.RemoveAllListeners();
    }
}
