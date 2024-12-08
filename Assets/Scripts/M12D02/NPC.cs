using System;
using UnityEngine;
using UnityEngine.AI;

namespace M12D02
{
    public class NPC : MonoBehaviour
    {
        [Header("目标")]
        public Transform target;

        private NavMeshAgent agent;


        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            agent.SetDestination(target.position);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                agent.isStopped = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Door"))
            {
                print("onTriggerEnter...");
                agent.isStopped = true;
            }
        }
    }
}
