using System;
using UnityEngine;

namespace M11D22
{
    // 机身姿势
    enum PlanePosture
    {
        LeftRotate, // 左翻转
        Horizontal, // 保持水平
        RightRotate // 右翻转
    }

    public class Player : MonoBehaviour
    {
        public static Player Instance;
        public const string Name = "Player";

        [Header("飞机前后左右移动速度")]
        public float moveSpeed;
        [Header("飞机水平向左移动的最大值")]
        public float maxLeftMove;
        [Header("飞机水平向右移动的最大值")]
        public float maxRightMove;
        [Header("飞机水平向上移动的最大值")]
        public float maxUpMove;
        [Header("飞机水平向下移动的最大值")]
        public float maxDownMove;
        [Header("玩家飞机射出的子弹")]
        public GameObject playerBullet;
        [Header("我方飞机爆炸特效")]
        public GameObject explosionPrefab;
        [Header("爆炸音效")]
        public AudioClip explodeSound;
        [Header("开火音效")]
        public AudioClip fireSound;
        
        // 机身姿势，默认保持水平
        private PlanePosture _planePosture;
        // 机身水平飞行时的欧拉角
        private Vector3 _localEulerAngles;
        // 血量
        private int _hp;
        // 大招，默认有3次
        private int _daZhao;
        // 死亡音效
        private AudioSource _audioSource;

        void Awake()
        {
            Instance = this;
            _planePosture = PlanePosture.Horizontal;
            _localEulerAngles = transform.localEulerAngles;
            _hp = 1;
            _daZhao = 3;
            _audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            Fire();
        }

        void Move()
        {
            float deltaDistance = moveSpeed * Time.deltaTime;
            float hor = Input.GetAxis("Horizontal");
            if (hor < 0)
                _planePosture = PlanePosture.LeftRotate;
            else if (hor > 0)
                _planePosture = PlanePosture.RightRotate;
            else
                _planePosture = PlanePosture.Horizontal;
            
            // 移动
            Vector3 position = transform.position;
            position += new Vector3(hor * deltaDistance, Input.GetAxis("Vertical") * deltaDistance, 0f);
            
            // 控制上下左右移动的范围
            position.x = Mathf.Clamp(position.x, maxLeftMove, maxRightMove);
            position.y = Mathf.Clamp(position.y, maxDownMove, maxUpMove);
            transform.position = position;
            
            // 在左右移动时，控制机身左右翻转
            transform.localEulerAngles = (_planePosture != PlanePosture.Horizontal)
                ? _localEulerAngles + Vector3.down * (hor * deltaDistance * 50)
                : _localEulerAngles;

        }

        // 按下鼠标左键开火
        void Fire()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ObjectPool.Instance.GetObject(PlayerBullet.Name, playerBullet, transform.position, Quaternion.identity);
                PlayFireSound();
            }
        }

        // 是否死亡
        public bool IsDead() => _hp <= 0;
        // 是否还有大招
        public bool HasDaZhao() => _daZhao > 0;
        // 加血
        public void AddHp(int hp)
        {
            if (IsDead())
                _hp = 0;
            
            _hp += hp;
        }
        // 失血
        public void SubHp(int hp)
        {
            if (IsDead())
            {
                _hp = 0;
                return;
            }
            _hp -= hp;
        }

        // 获得大招
        public void AddDaZhao(int daZhao)
        {
            if (_daZhao < 0) _daZhao = 0;
            _daZhao += daZhao;
        }

        // 使用大招
        public void SubDaZhao(int daZhao)
        {
            if (_daZhao <= 0)
            {
                _daZhao = 0;
                return;
            }
            _daZhao -= daZhao;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EnemyBullet") || other.CompareTag("EnemyShip") || other.CompareTag("Asteroid"))
            {
                SubHp(1);
            }

            if (IsDead())
            {
                ObjectPool pool = ObjectPool.Instance;
                _daZhao = 0;
                // 播放死亡音效
                PlayExplodeSound();
                // 播放爆炸特效
                pool.GetObject(PlayerExplosion.Name, explosionPrefab, transform.position, Quaternion.identity);
                // 回收
                pool.PutObject(Name, gameObject);
            }
        }
        
        // 播放爆炸音频
        void PlayExplodeSound()
        {
            _audioSource.clip = explodeSound;
            _audioSource.Play();
        }

        // 播放开火音频
        void PlayFireSound()
        {
            _audioSource.clip = fireSound;
            _audioSource.Play();
        }
    }
}