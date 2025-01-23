using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolBomb : MonoBehaviour
{

    public GameObject objectPrefab;
    public int poolSize = 10;

    public Queue<GameObject> PoolQueue { get; private set; }

    // Singleton
    #region Singleton
    private static ObjectPoolBomb _instance;

    public static ObjectPoolBomb Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("ObjectPoolBomb");
                _instance = go.AddComponent<ObjectPoolBomb>();
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

    public void Start()
    {
        PoolQueue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.name = objectPrefab.name + i.ToString();
            obj.transform.position -= Vector3.forward * (1f * i);
            obj.SetActive(false);
            obj.TryGetComponent(out Bomb bomb);
            bomb.OnExplode += ScreenJuice.Instance.BigScreenShake;
            PoolQueue.Enqueue(obj);
        }

        while (SpawnerBomb.Instance.spawnCount < 5)
        {
            SpawnerBomb.Instance.OnSpawnBomb();
        }
    }

    private void Bomb_OnExplode()
    {
        throw new System.NotImplementedException();
    }

    public GameObject GetBomb()
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
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.TryGetComponent(out Bomb bomb);
        bomb.Collider.isTrigger = true;
        PoolQueue.Enqueue(obj);
        SpawnerBomb.Instance.OnSpawnBomb();
    }
}
