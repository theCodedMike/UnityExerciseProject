using UnityEngine;

namespace M11D22
{
    public class Main : MonoBehaviour
    {
        [Header("己方飞机")]
        public GameObject playerShip;
        [Header("敌方飞机")]
        public GameObject enemyShip;
        
        private Camera _mainCam;

        private float _timer;
        private float _randomInterval;
        
        // Start is called before the first frame update
        private void Start()
        {
            _mainCam = Camera.main;
            GenerateAPlayer();
        }
        
        // 在屏幕正中心生成一个我方飞机
        void GenerateAPlayer()
        {
            Ray ray = _mainCam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
            ObjectPool.GetInstance().GetObject(Player.Name, playerShip, new Vector3(ray.origin.x, ray.origin.y, 0),
                Quaternion.Euler(-90, 0, 0));
        }

        
        // Update is called once per frame
        void Update()
        {
            RandomGenerateManyEnemy();
        }

        // 随机生成大量敌方飞机
        void RandomGenerateManyEnemy()
        {
            _timer += Time.deltaTime;
            if (_timer >= _randomInterval)
            {
                _timer = 0;
                _randomInterval = Random.Range(3, 5); // 每隔3-5秒生成大量飞机，每次生成3-5架飞机
                for (int i = 0, count = Random.Range(3, 6); i < count; i++)
                {
                    float x = Random.Range(Screen.width * 0.1f, Screen.width * 0.9f);
                    Ray ray = _mainCam.ScreenPointToRay(new Vector3(x, Screen.height, 0));
                    GameObject clone = ObjectPool.GetInstance().GetObject(Enemy.Name, enemyShip, new Vector3(ray.origin.x, ray.origin.y, 0), Quaternion.Euler(90, 0, -180));
                    print($"clone: {clone.transform.position}");
                }
            }
        }
    }
}