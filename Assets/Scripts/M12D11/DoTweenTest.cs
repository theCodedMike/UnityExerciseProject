using UnityEngine;
using DG.Tweening;

namespace M12D11
{
    public class DoTweenTest : MonoBehaviour
    {
        public Transform target;

        public float value;


        private Camera mainCamera;

        // Start is called before the first frame update
        void Start()
        {
            mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
               
                //MoveTest();
                SequenceTest();
            }
        }

        void MoveTest()
        {
               transform.DOMove(target.position, 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutBack);
               DOTween.To(() => value, x => value = x, 100, 3f).OnUpdate(() => print(value)).OnComplete(() => print("Complete"));
               mainCamera.DOShakeRotation(3f);
        }

        void SequenceTest()
        {
            // Grab a free Sequence to use
            Sequence mySequence = DOTween.Sequence(); // sequence不能提前生成，必须这样写
            // Add a movement tween at the beginning
            mySequence.Append(transform.DOMoveX(15, 1));
            // Add a rotation tween as soon as the previous one is finished
            mySequence.Append(transform.DORotate(new Vector3(0, 180, 0), 1));
            // Delay the whole Sequence by 1 second
            //mySequence.PrependInterval(1);
            // Insert a scale tween for the whole duration of the Sequence
            //mySequence.Insert(0, transform.DOScale(new Vector3(3, 3, 3), mySequence.Duration()));
        }
    }
}
