using UnityEngine;

namespace M12D05
{
    // 我自己写的
    public class Person : MonoBehaviour
    {
        public string personName; // 名字
        public Role role;   //角色
        public State state; // 状态
        public int hp; // 血量
        public float moveSpeed; // 移动速度

        private Vector3 direction = Vector3.forward;

        private float timer;
        // Start is called before the first frame update
        void Start()
        {
            timer = Random.Range(1f, 2f);
        }

        // Update is called once per frame
        void Update()
        {
            Move();

            Logic();
        }

        void Logic()
        {
            if (state == State.Patrol)
            {

            }
            else if (state == State.Attack)
            {
                if (hp < 10) // 血快掉光了，逃跑
                {
                    state = State.Running;

                }
            }
            else if (state == State.Running)
            {

            }
        }

        void Move()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = Random.Range(3f, 5f);

            genDir:
                Vector3 newDir = Random.Range(1, 5) switch
                {
                    1 => Vector3.forward,
                    2 => Vector3.back,
                    3 => Vector3.left,
                    4 => Vector3.right,
                    _ => Vector3.zero,
                };

                if (newDir == direction)
                    goto genDir;
                direction = newDir;
            }

            transform.Translate(direction * moveSpeed);
            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, -100, 100);
            position.z = Mathf.Clamp(position.z, -100, 100);
            transform.position = position;
        }
    }

    [System.Serializable]
    public enum State
    {
        Running, // 逃跑
        Patrol, // 巡逻
        Attack,
    }

    [System.Serializable]
    public enum Role
    {
        Player, // 玩家
        Enemy, // 敌人
    }
}
