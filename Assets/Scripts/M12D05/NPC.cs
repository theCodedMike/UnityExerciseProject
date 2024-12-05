using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.M12D05
{
    // 这是老师写的
    public class NPC : MonoBehaviour
    {
        [Header("移动路径")]
        public Transform[] paths;
        [Header("移动速度")]
        public float moveSpeed;


        private Transform player;
        private int index = 0;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.SqrMagnitude(player.position - transform.position) < 16)
            {
                Follow();
            }
            else
            {
                Patrol();
            }
        }

        // 巡逻
        void Patrol()
        {
            transform.position =
                Vector3.MoveTowards(transform.position, paths[index].position, Time.deltaTime * moveSpeed);
            if (Vector3.SqrMagnitude(transform.position - paths[index].position) < 0.01f)
            {
                index++;
                index %= paths.Length;
            }
        }

        // 跟随
        void Follow()
        {
            if (Vector3.SqrMagnitude(transform.position - player.position) < 1)
            {
                Attack();
            }
            else
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * moveSpeed);
            }
        }

        // 攻击
        void Attack()
        {
            print("攻击玩家");
        }
    }
}
