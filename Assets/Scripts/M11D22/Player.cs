using UnityEngine;
using UnityEngine.Serialization;

namespace M11D22
{
    enum PlanePosture
    {
        LeftRotate,
        Horizontal,
        RightRotate
    }

    public class Player : MonoBehaviour
    {
        public static Player Instance;
        
        [Header("飞机前后左右移动速度")]
        public float moveSpeed = 20f;
        [Header("飞机水平向左移动的最大值")]
        public float maxLeftMove = -8f;
        [Header("飞机水平向右移动的最大值")]
        public float maxRightMove = 8f;
        [Header("飞机水平向上移动的最大值")]
        public float maxUpMove = 4.5f;
        [Header("飞机水平向下移动的最大值")]
        public float maxDownMove = -4f;
        
        
        // 机身姿势，默认保持水平
        private PlanePosture _planePosture = PlanePosture.Horizontal;
        // 机身水平飞行时的欧拉角
        private Vector3 _localEulerAngles;

        void Awake()
        {
            Instance = this;
        }


        // Start is called before the first frame update
        void Start()
        {
            _localEulerAngles = transform.localEulerAngles;
            print($"localEulerAngles: {_localEulerAngles}");
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        void Move()
        {
            float deltaDistance = moveSpeed * Time.deltaTime;
            float hor = Input.GetAxis("Horizontal");
            if (hor < 0)
                _planePosture = PlanePosture.LeftRotate;
            else if (hor > 0)
                _planePosture = PlanePosture.RightRotate;
            else
                _planePosture = PlanePosture.Horizontal;
            
            // 移动
            Vector3 position = transform.position;
            position += new Vector3(hor * deltaDistance, Input.GetAxis("Vertical") * deltaDistance, 0f);
            
            // 控制上下左右移动的范围
            position.x = Mathf.Clamp(position.x, maxLeftMove, maxRightMove);
            position.y = Mathf.Clamp(position.y, maxDownMove, maxUpMove);
            transform.position = position;
            
            // 在左右移动时，控制机身左右翻转
            
            if (_planePosture != PlanePosture.Horizontal)
                transform.localEulerAngles = _localEulerAngles + Vector3.down * (hor * deltaDistance * 50);
        }
    }
}