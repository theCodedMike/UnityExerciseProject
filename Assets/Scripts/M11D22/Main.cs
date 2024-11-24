using UnityEngine;

namespace M11D22
{
    public class Main : MonoBehaviour
    {
        [Header("我方飞机预制体")] 
        public GameObject playerShip;
        [Header("敌方飞机预制体")] 
        public GameObject enemyShip;
        [Header("小行星预制体")] 
        public GameObject[] asteroids;
        [Header("大招预制体")]
        public GameObject daZhao;
        [Header("冲击波预制体")]
        public GameObject shockwave;
        
        private Camera _mainCam;
        private float _genEnemyTimer; // 生成敌方飞机的间隔时间
        private int _countPerGen = 1; // 每次生成飞机的个数
        private Ray _centerPoint; // 屏幕正中心位置
        private Ray _rightUpPoint; // 屏幕右上方，即宽高最大值

        // Start is called before the first frame update
        private void Start()
        {
            _mainCam = Camera.main;
            _centerPoint = _mainCam!.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
            _rightUpPoint = _mainCam!.ScreenPointToRay(new Vector3(Screen.width, Screen.height, 0));
            
            GenerateAPlayer();
        }

        // 在屏幕正中心生成一个我方飞机
        void GenerateAPlayer()
        {
            Vector3 position = new Vector3(_centerPoint.origin.x, _centerPoint.origin.y, 0);
            ObjectPool.Instance.GetObject(Player.Name, playerShip, position, Quaternion.identity);
            ObjectPool.Instance.GetObject(Shockwave.Name, shockwave, position, Quaternion.identity);
        }


        // Update is called once per frame
        void Update()
        {
            
            if (Player.Instance.IsDead())
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GenerateAPlayer();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) && Player.Instance.HasDaZhao())
                {
                    GenerateADaZhao();
                }
                RandomGenerateManyEnemy();
                RandomGenerateManyAsteroid();
            }
            //print($"到目前为止，生成了{_enemyCount}架敌机，Pool中有{ObjectPool.Instance.GetCount(Enemy.Name)}架敌机");
            //print($"我方产生的子弹个数: {ObjectPool.Instance.GetCount(PlayerBullet.Name)}");
        }

        // 随机生成大量敌方飞机
        void RandomGenerateManyEnemy()
        {
            if (_genEnemyTimer >= 0)
                _genEnemyTimer -= Time.deltaTime;
            else
            {
                if (_countPerGen > 0)
                {
                    float x = Random.Range(Screen.width * 0.1f, Screen.width * 0.9f);
                    float y = Random.Range(Screen.height, Screen.height * 1.2f);
                    Ray ray = _mainCam.ScreenPointToRay(new Vector3(x, y, 0));
                    ObjectPool.Instance.GetObject(Enemy.Name, enemyShip,
                        new Vector3(ray.origin.x, ray.origin.y, 0), Quaternion.Euler(90, 0, -180));
                    _countPerGen -= 1;
                }
                else
                {
                    _genEnemyTimer = Random.Range(3f, 5f); // 每隔3-5秒生成大量飞机，每次生成2-3架飞机
                    _countPerGen = Random.Range(2, 4);
                }
            }
        }

        // 随机生成大量小行星
        void RandomGenerateManyAsteroid()
        {
            if (_genEnemyTimer >= 0)
                _genEnemyTimer -= Time.deltaTime;
            else
            {
                if (_countPerGen > 0)
                {
                    float x = Random.Range(Screen.width * 0.1f, Screen.width * 0.9f);
                    float y = Random.Range(Screen.height, Screen.height * 1.2f);
                    Ray ray = _mainCam.ScreenPointToRay(new Vector3(x, y, 0));
                    ObjectPool.Instance.GetObject(Asteroid.Name, asteroids[Random.Range(0, asteroids.Length)],
                        new Vector3(ray.origin.x, ray.origin.y, 0), Quaternion.identity);
                    _countPerGen -= 1;
                }
                else
                {
                    _genEnemyTimer = Random.Range(3f, 5f); // 每隔3-5秒生成大量小行星，每次生成2-3颗
                    _countPerGen = Random.Range(2, 4);
                }
            }
        }

        // 发放大招
        void GenerateADaZhao()
        {
            Vector3 position = new Vector3(_centerPoint.origin.x, Player.Instance.transform.position.y, 0);
            ObjectPool.Instance.GetObject(DaZhao.Name, daZhao, position, Quaternion.identity);
            Player.Instance.SubDaZhao(1);
        }
    }
}