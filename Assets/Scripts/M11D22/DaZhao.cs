using UnityEngine;

public class DaZhao : MonoBehaviour
{
    public const string Name = "DaZhao";
    
    [Header("移动速度")]
    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * (moveSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            // 回收
            Invoke(nameof(Disable), 0f);
        }
    }
    
    void Disable()
    {
        ObjectPool.Instance.PutObject(Name, gameObject);
    }
}
