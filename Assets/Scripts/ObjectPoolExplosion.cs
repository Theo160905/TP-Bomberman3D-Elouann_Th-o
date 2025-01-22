using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolExplosion : MonoBehaviour
{
    public GameObject objectPrefab;
    public int poolSize = 10;

    public Queue<GameObject> PoolQueue { get; private set; }

    // Singleton
    #region Singleton
    private static ObjectPoolExplosion _instance;

    public static ObjectPoolExplosion Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("ObjectPoolExplosion");
                _instance = go.AddComponent<ObjectPoolExplosion>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    void Start()
    {
        PoolQueue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.transform.parent = transform;
            obj.name = objectPrefab.name + i.ToString();
            obj.transform.position -= Vector3.forward * (1f * i);
            obj.SetActive(false);
            PoolQueue.Enqueue(obj);
        }
    }

    public GameObject GetObject(GameObject gameObject)
    {
        if (PoolQueue.Count > 0)
        {
            GameObject obj = PoolQueue.Dequeue();
            obj.gameObject.transform.position = gameObject.transform.position;
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.transform.parent = transform;
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        PoolQueue.Enqueue(obj);
    }
}
