using UnityEngine;

namespace M11D22
{
    public class Main : MonoBehaviour
    {
        [Header("己方飞机")]
        public GameObject playerShip;

        private Camera _mainCam;
        // Start is called before the first frame update
        private void Start()
        {
            _mainCam = Camera.main;
           RandomGenerateAPlayer();
        }
        
        // 随机生成一个己方飞机
        void RandomGenerateAPlayer()
        {
            float width = Random.Range(Screen.width * 0.2f, Screen.width * 0.8f);
            float height = Random.Range(Screen.height * 0.1f, Screen.height * 0.4f);
            Ray ray = _mainCam.ScreenPointToRay(new Vector3(width, height, 0));
            GameObject clone = Instantiate(playerShip, new Vector3(ray.origin.x, ray.origin.y, 0), Quaternion.Euler(-90, 0, 0));
            print($"clone position: {clone.transform.position}");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}