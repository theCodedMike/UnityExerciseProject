using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.M12D05
{
    public class ParticleTest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void OnParticleCollision(GameObject other)
        {
            print("other: " + other.name);
        }

        void OnTriggerEnter(Collider other)
        {
            print("enter: " + other.name);
        }
    }
}
