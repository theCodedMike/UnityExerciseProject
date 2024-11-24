using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace M11D22
{
    enum MoveDirection
    {
        Down, // 向下 
        Left, // 向左
        Right, // 向右
        LeftDown, // 左下
        RightDown, // 右下
        Freeze // 保持不动
    }
    public class Enemy : MonoBehaviour
    {
        public const string Name = "EnemyShip";

        [Header("飞机前后左右的移动速度")] 
        public float moveSpeed;
        [Header("飞机水平向左移动的最大值")] 
        public float maxLeftMove;
        [Header("飞机水平向右移动的最大值")] 
        public float maxRightMove;
        [Header("敌方飞机射出的子弹")] 
        public GameObject enemyBullet;
        [Header("敌机爆炸特效")]
        public GameObject explosionPrefab;
        [Header("爆炸音效")]
        public AudioClip explodeSound;
        [Header("开火音效")]
        public AudioClip fireSound;

        private Vector3 _moveDirection; // 飞机移动距离
        private MoveDirection _moveDir; // 飞机移动方向
        private float _moveTimer; // 每隔几秒转变一次移动方向
        private float _launchBulletTimer; // 发射子弹的间隔时间
        private int _countPerLaunch; // 每次发射多少颗子弹
        private AudioSource _audioSource; // 音频

        private void Awake()
        {
            _moveDirection = Vector3.down * 5;
            _moveDir = MoveDirection.Down;
            _moveTimer = 2;
            _launchBulletTimer = 0;
            _countPerLaunch = 3;
            _audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            float deltaTime = Time.deltaTime;
            Move(deltaTime);
            Fire(deltaTime);
            //print($"xxxxxxx敌机生成了{EnemyBullet.Count}颗子弹，Pool中有{ObjectPool.Instance.GetCount(EnemyBullet.Name)}颗子弹");
        }

        // 飞机的移动
        void Move(float deltaTime)
        {
            if (_moveTimer > 0)
            {
                Vector3 position = Vector3.MoveTowards(transform.position, transform.position + _moveDirection,
                    moveSpeed * deltaTime);
                position.x = Mathf.Clamp(position.x, maxLeftMove, maxRightMove);
                transform.position = position;
                _moveTimer -= deltaTime;
            }
            else
            {
                _moveTimer = Random.Range(2f, 3f);
                _moveDirection = GetMoveDirection();
            }
        }

        // 控制敌方飞机的移动方向
        Vector3 GetMoveDirection()
        {
            Vector3 result;
            MoveDirection oldMoveDir = _moveDir;
            
            start:
            float moveStep = Random.Range(8f, 12f);
            switch(Random.Range(0f, 30f))
            {
                case < 5:
                    _moveDir = MoveDirection.Down; result = Vector3.down * moveStep; break;// 向下
                case < 10: 
                    _moveDir = MoveDirection.Left; result = Vector3.left * moveStep; break; // 向左
                case < 15: 
                    _moveDir = MoveDirection.Right; result = Vector3.right * moveStep; break; // 向右
                case < 20: 
                    _moveDir = MoveDirection.LeftDown; result = new Vector3(-moveStep, -moveStep, 0); break; // 左下
                case < 25: 
                    _moveDir = MoveDirection.RightDown; result = new Vector3(moveStep, -moveStep, 0); break; // 右下
                default: 
                    _moveDir = MoveDirection.Freeze; result = Vector3.zero; break;// 保持不动
            }

            if (oldMoveDir == _moveDir)
                goto start;
            
            return result;
        }

        // 发射子弹
        void Fire(float deltaTime)
        {
            if (_countPerLaunch > 0)
            {
                _launchBulletTimer += deltaTime;
                if (_launchBulletTimer >= 1)
                {
                    _launchBulletTimer = 0;
                    _countPerLaunch--;
                    ObjectPool.Instance.GetObject(EnemyBullet.Name, enemyBullet, transform.position, Quaternion.identity);
                    PlayFireSound();
                }    
            }
            else
            {
                _countPerLaunch = Random.Range(2, 4);
            }
            
            /*if (_launchBulletTimer >= 0)
                _launchBulletTimer -= deltaTime;
            else
            {
                // 每隔2-3秒发射3-4颗子弹
                if (_countPerLaunch > 0)
                {
                    ObjectPool.Instance.GetObject(EnemyBullet.Name, enemyBullet, transform.position, Quaternion.identity);
                    _countPerLaunch--;
                    _enemyBulletCount++;
                }
                else
                {
                    _countPerLaunch = Random.Range(2, 4);
                    _launchBulletTimer = Random.Range(5f, 7f);
                }
            }*/
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground") || other.CompareTag("DaZhao") || other.CompareTag("Shockwave"))
            {   // 回收
                Invoke(nameof(Disable), 0f);
            } else if (other.CompareTag("PlayerBullet"))
            {   // 碰到我方子弹
                Player.Instance.AddHp(1);
                PlayExplodeSound();
                GameObject obj = ObjectPool.Instance.GetObject(EnemyExplosion.Name, explosionPrefab, transform.position, Quaternion.identity);
                Invoke(nameof(Disable), 0f);
                // TODO: 敌机爆炸特效实例是否可回收
                //ObjectPool.Instance.PutObject(EnemyExplosion.Name, obj);
            }
        }
        
        void Disable()
        {
            ObjectPool.Instance.PutObject(Name, gameObject);
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