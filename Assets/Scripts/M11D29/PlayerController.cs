using System.Collections;
using UnityEngine;

namespace M11D29
{
    // ReSharper disable once HollowTypeName
    public class PlayerController : MonoBehaviour
    {
        [Header("玩家")]
        public Transform player;
        [Header("小球")]
        public Transform[] spheres;

        public float speed = 10;

        private int currIdx = 0;


        //private int nextIdx = 1;
        // Start is called before the first frame update
        void Start()
        {
            //player.position = spheres[currIdx].position;
            InvokeRepeating("Move", 0f, 2f);
            //StartCoroutine(Go());
        }

        // Update is called once per frame
        void Update()
        {


        }

        // 这么写不行
        void Move()
        {
            player.position = Vector3.MoveTowards(player.position, spheres[currIdx].transform.position, Time.deltaTime * speed);
            spheres[currIdx].GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
            currIdx++;
            currIdx %= spheres.Length;
        }

        IEnumerator Go()
        {
            while (true)
            {
                player.position = Vector3.MoveTowards(player.position, spheres[currIdx].position, Time.deltaTime * speed);
                if (Vector3.SqrMagnitude(player.position - spheres[currIdx].position) < 0.01f)
                {
                    spheres[currIdx].GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
                    currIdx++;
                    currIdx %= spheres.Length;

                    yield return new WaitForSeconds(2f);
                }

                yield return 0;
            }
        }
    }
}
