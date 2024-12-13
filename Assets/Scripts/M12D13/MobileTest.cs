using UnityEngine;
using TMPro;

namespace M12D13
{
    public class MobileTest : MonoBehaviour
    {
        
        public TMP_Text m_Text;

        private MeshRenderer cubeMR;

        private Swipe swipe = new Swipe();
        // Start is called before the first frame update
        void Start()
        {
            cubeMR = GetComponent<MeshRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
#if UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                //SingleFingerTouch();
                //MultiFingerTouch();
                MoveTest();
            }
#endif
        }

        private void MoveTest()
        {
            Touch touch = Input.GetTouch(0);
            print(touch);
            if (touch.phase == TouchPhase.Began)
            {
                swipe.isTouch = true;
                swipe.startPosition = touch.position;
            } else if (touch.phase is TouchPhase.Moved or TouchPhase.Stationary)
            {
                if (!swipe.isTouch) return;

                swipe.endPosition = touch.position;
                Direction direction = swipe.Parse();
                if (direction == Direction.None)
                    return;

                m_Text.text = direction.ToString();
            } else if (touch.phase == TouchPhase.Ended)
            {
                swipe.Clear();
            }
        }


        void SingleFingerTouch()
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                cubeMR.material.color = Color.green;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                cubeMR.material.color = Color.yellow;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                cubeMR.material.color = Color.red;
            }
        }

        void MultiFingerTouch()
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    cubeMR.material.color = Color.green;
                }
                else
                {
                    cubeMR.material.color = Color.red;
                }
            }
        }
    }

    public enum Direction
    {
        None, Up, Down, Left, Right
    }

    public class Swipe
    {
        public Vector2 startPosition;
        public Vector2 endPosition;
        public bool isTouch { get; set; }

        public Direction Parse()
        {
            if (!isTouch)
                return Direction.None;

            Direction dir = Direction.None;

            Vector2 v = endPosition - startPosition;
            if (Vector2.SqrMagnitude(v) < Mathf.Pow(Screen.width / 8, 2))
                return Direction.None;

            float maxValue = 0;
            float value = Vector2.Dot(v, Vector2.up);
            if (value > maxValue)
            {
                dir = Direction.Up;
                maxValue = value;
            }
            value = Vector2.Dot(v, Vector2.down);
            if (value > maxValue)
            {
                dir = Direction.Down;
                maxValue = value;
            }
            value = Vector2.Dot(v, Vector2.right);
            if (value > maxValue)
            {
                dir = Direction.Right;
                maxValue = value;
            }
            value = Vector2.Dot(v, Vector2.left);
            if (value > maxValue)
            {
                dir = Direction.Left;
                maxValue = value;
            }

            return dir;
        }

        public void Clear()
        {
            isTouch = false;
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
        }
    }
}
