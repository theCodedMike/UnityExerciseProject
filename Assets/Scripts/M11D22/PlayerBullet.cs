using UnityEngine;

namespace M11D22
{
    public class PlayerBullet : MonoBehaviour
    {
        public const string Name = "PlayerBullet";
        
        [Header("移动速度")]
        public float moveSpeed = 20f;

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.up * (Time.deltaTime * moveSpeed));
        }
        
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground"))
            {   // 回收
                Invoke(nameof(Disable), 0f);
            }
        }
        
        void Disable()
        {
            ObjectPool.Instance.PutObject(Name, gameObject);
        }
    }
}
