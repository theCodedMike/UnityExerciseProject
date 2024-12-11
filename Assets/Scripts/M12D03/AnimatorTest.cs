using UnityEngine;

namespace M12D03
{
    public class AnimatorTest : MonoBehaviour
    {
        private static readonly int Direction = Animator.StringToHash("Direction");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Back = Animator.StringToHash("Back");
        private static readonly int Slide = Animator.StringToHash("Slide");
        private static readonly int IsHoldLog = Animator.StringToHash("IsHoldLog");


        private Animator animator;
        private float v;

        private bool isHoldLog;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            float h = Input.GetAxis("Horizontal");
            animator.SetFloat(Direction, h);
            
            v = Input.GetAxis("Vertical");
            if (Input.GetKey(KeyCode.LeftControl))
            {
                v += 0.5f;
            }
            animator.SetFloat(Speed, v);

            bool back = v < 0;
            animator.SetBool(Back, back);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger(Slide);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                isHoldLog = !isHoldLog;
                animator.SetBool(IsHoldLog, isHoldLog);
            }
        }
    }
}
