using UnityEngine;

namespace M11D22
{
    public class Asteroid : MonoBehaviour
    {
        public const string Name = "Asteroid";
        
        [Header("移动速度")]
        public float moveSpeed;
        [Header("旋转速度")]
        public float rotateSpeed;
        [Header("小行星水平向左移动的最大值")]
        public float maxLeftMove;
        [Header("小行星水平向右移动的最大值")]
        public float maxRightMove;
        [Header("小行星爆炸特效")]
        public GameObject explosionPrefab;
        
        private Vector3 _rotation;
        private AudioSource _deadAudio; // 死亡音频
        
        void Awake()
        {
            _deadAudio = GetComponent<AudioSource>();
            _rotation = new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), 0);
        }
        // Update is called once per frame
        void Update()
        {
            transform.Rotate(_rotation * (Time.deltaTime * rotateSpeed), Space.Self);
            transform.Translate(Vector3.down * (Time.deltaTime * moveSpeed));
            Vector3 position = transform.position;
            position.z = 0;
            position.x = Mathf.Clamp(position.x, maxLeftMove, maxRightMove);
            transform.position = position;
        }
        
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground") || other.CompareTag("DaZhao") || other.CompareTag("Shockwave"))
            {   // 回收
                Invoke(nameof(Disable), 0f);
            }else if (other.CompareTag("PlayerBullet"))
            {   // 碰到我方子弹
                Player.Instance.AddHp(1);
                _deadAudio.Play();
                GameObject obj = ObjectPool.Instance.GetObject(AsteroidExplosion.Name, explosionPrefab, transform.position, Quaternion.identity);
                Invoke(nameof(Disable), 0f);
                // TODO: 小行星爆炸特效实例是否可回收
                //ObjectPool.Instance.PutObject(AsteroidExplosion.Name, obj);
            }
        }
        
        void Disable()
        {
            ObjectPool.Instance.PutObject(Name, gameObject);
        }
    }
}
