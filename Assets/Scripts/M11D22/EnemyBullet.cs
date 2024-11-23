using UnityEngine;

namespace M11D22
{
    public class EnemyBullet : MonoBehaviour
    {
        public const string Name = "EnemyBullet";
        [Header("移动速度")]
        public float moveSpeed = 20f;

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.down * (Time.deltaTime * moveSpeed));
        }
    }
}
