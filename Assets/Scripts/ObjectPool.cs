using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private static ObjectPool _instance;
    //private static readonly Object Lock = new();
    public static ObjectPool Instance
    {

        get { return _instance ??= new ObjectPool(); }
    }
    
    private readonly Dictionary<string, Queue<GameObject>> _pool;
    
    /*public static ObjectPool GetInstance()
    {
        if (_instance == null)
        {
            lock (Lock)
            {
                if(_instance == null)
                {
                    _instance = new ObjectPool();
                }
            }
        }

        return _instance;
    }*/

    private ObjectPool()
    {
        _pool = new Dictionary<string, Queue<GameObject>>();
    }


    public void PutObject(string key, GameObject obj)
    {
        if (!_pool.ContainsKey(key))
            _pool.Add(key, new Queue<GameObject>());
        obj.SetActive(false);
        _pool[key].Enqueue(obj);
    }

    public GameObject GetObject(string key, GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject result = null;
        if (_pool.ContainsKey(key) && _pool[key].Count != 0)
        {
            result = _pool[key].Dequeue();
            result.SetActive(true);
        }
        else
        {
            result = Object.Instantiate(prefab);
        }
        result.transform.position = position;
        result.transform.rotation = rotation;

        return result;
    }

    // 获取某种类型的游戏对象的个数
    public int GetCount(string key) => _pool.ContainsKey(key) ? _pool[key].Count : 0;

}
