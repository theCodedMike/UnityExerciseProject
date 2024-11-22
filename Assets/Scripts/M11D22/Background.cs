using System;
using UnityEngine;

namespace M11D22
{
    public class Background : MonoBehaviour
    {
        private static readonly String MainTex = "_MainTex";
        
        [Header("背景移动速度")]
        public float moveSpeed = 20f;
        
        private Material _meshMaterial;
        
        // Start is called before the first frame update
        void Start()
        {
            _meshMaterial = transform.GetComponent<MeshRenderer>().material;
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 currOffset = _meshMaterial.GetTextureOffset(MainTex);
            _meshMaterial.SetTextureOffset(MainTex, currOffset + Vector2.up * (Time.deltaTime * moveSpeed));
        }
        
        
    }
}
