using System.Collections;
using UnityEngine;

namespace Assets.Scripts.M11D29
{
    public class Test : MonoBehaviour
    {
        private MeshRenderer m_Renderer;

        // Start is called before the first frame update
        void Start()
        {
            m_Renderer = GetComponent<MeshRenderer>();


            print("K");

            // 没有渐变的效果
            //Fade1();

            // 有渐变的效果
            //StartCoroutine(Fade2());
            StartCoroutine(Fade3());
            print("N");
        }

        // Update is called once per frame
        void Update()
        {
            //print("U");
        }


        // 每秒减少0.1，所以需要持续10秒
        IEnumerator Fade3()
        {
            for (float f = 1f; f >= 0; f -= 0.1f)
            {
                Color color = m_Renderer.material.color;
                color.a = f;
                m_Renderer.material.color = color;
                yield return new WaitForSeconds(1f);
            }
        }

        // 每帧减少0.005，所以需要持续200帧，大约200/60=3.33秒
        IEnumerator Fade2()
        {
            for (float f = 1f; f >= 0; f -= 0.005f)
            {
                Color color = m_Renderer.material.color;
                color.a = f;
                m_Renderer.material.color = color;
                yield return null;
            }
        }


        // 在单帧内执行完，所以看不到渐变的过程
        void Fade1()
        {
            Color c = m_Renderer.material.color;
            for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
            {
                c.a = alpha;
                m_Renderer.material.color = c;
            }
        }
    }
}
