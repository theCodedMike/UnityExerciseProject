using UnityEngine;

namespace M12D03
{
    public class AnimationTest : MonoBehaviour
    {
        private Animation anim;
        private AudioSource audioSource;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animation>();
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.Play("Move");
            } else if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                anim.Play("Rotate");
            }
        }




        void OnArriveTop()
        {
            print("Arrive top: " + transform.position);
            audioSource.Play();
        }
    }
}
