using UnityEngine;
using Random = UnityEngine.Random;

namespace M11D22
{
    public class Enemy : MonoBehaviour
    {
        public const string Name = "Enemy";
        
        [Header("飞机前后左右移动速度")]
        public float moveSpeed = 50f;

        private float _timer; // 飞机每隔几秒移动一次
        private float _randomInterval;
        private Vector3 _moveDirection;
        
        private float _moveTimer = 2f;
        
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            float deltaTime = Time.deltaTime;
            if (_moveTimer >= 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + _moveDirection, 2f * deltaTime);
                _moveTimer -= deltaTime;
            }
            else
            {
                _moveTimer = Random.Range(2f, 3f);
                _moveDirection = GetMoveDirection(deltaTime);
            }
        }
        
        Vector3 GetMoveDirection(float deltaTime) => Random.Range(0, 20) switch
        {
            < 5 => new Vector3(0, -deltaTime * moveSpeed, 0), // 向下
            < 10 => new Vector3(-deltaTime * moveSpeed, 0, 0), // 向左
            < 15 => new Vector3(deltaTime * moveSpeed, 0, 0), // 向右
            _ => Vector3.zero,
        };
    }
}
