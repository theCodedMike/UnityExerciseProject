using System;
using M11D22;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Assets.Scripts.M12D05
{
    // 这是我自己的实现
    public class FSM_Test : MonoBehaviour
    {
        private GameObject enemyPrefab;

        private GameObject playerPrefab;

        // Start is called before the first frame update
        void Start()
        {
            enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
            playerPrefab = Resources.Load<GameObject>("Prefabs/Player");

            GenPlayer();

            GenEnemy();
        }

        void GenPlayer()
        {
            GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            Person p = player.GetComponent<Person>();
            p.personName = "Player";
            p.role = Role.Player;
            p.state = State.Patrol;
            p.hp = 100;
            p.moveSpeed = 0.06f;
        }


        void GenEnemy()
        {
            for (int i = 0; i < 30; i++)
            {
                Vector3 newPositon = Vector3.zero + new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
                GameObject cloned = Instantiate(enemyPrefab, newPositon, Quaternion.identity);
                cloned.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
                Person p = cloned.GetComponent<Person>();
                p.state = State.Patrol;
                p.hp = Random.Range(50, 100);
                p.moveSpeed = Random.Range(0.05f, 0.1f);
                p.personName = $"{cloned.name}{i + 1}";
                p.role = Role.Enemy;
            }
        }
    }
}
