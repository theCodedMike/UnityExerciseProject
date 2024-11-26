using System;
using UnityEngine;

namespace M11D22
{
    public class Background : MonoBehaviour
    {
        private static readonly int Tex = Shader.PropertyToID("_MainTex");
        
        
        [Header("背景移动速度")]
        public float moveSpeed = 20f;
        
        private Material _meshMaterial;
        private Vector2 _offset;
        
        // Start is called before the first frame update
        void Start()
        {
            _meshMaterial = transform.GetComponent<MeshRenderer>().sharedMaterial;
        }

        // Update is called once per frame
        void Update()
        {
            _offset += Vector2.up * (Time.deltaTime * moveSpeed);
            _meshMaterial.SetTextureOffset(Tex, _offset);
        }
        
        
    }
}
