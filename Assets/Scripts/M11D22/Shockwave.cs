using UnityEngine;

namespace M11D22
{
    public class Shockwave : MonoBehaviour
    {
        public const string Name = "Shockwave";
        
        private float _survivalTime; // 存活时间

        void Awake()
        {
            _survivalTime = 3f;
        }

        // Update is called once per frame
        void Update()
        {
            if (_survivalTime >= 0)
            {
                transform.position = Player.Instance.transform.position;
            }
            else
            {
                ObjectPool.Instance.PutObject(Name, gameObject);
            }
            _survivalTime -= Time.deltaTime;
        }
    }
}
