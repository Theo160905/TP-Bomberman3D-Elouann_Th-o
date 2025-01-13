using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ObjectPoolBomb : MonoBehaviour
{

    public GameObject objectPrefab;
    public int poolSize = 10;

    public float spawnRadius = 5f;

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
            Debug.Log($"<b><color=#{UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 0f, 1f, 1f).ToHexString()}>{this.GetType()}</color> instance <color=#eb624d>destroyed</color></b>");
        }
        else
        {
            _instance = this;
            Debug.Log($"<b><color=#{UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f, 1f, 1f).ToHexString()}>{this.GetType()}</color> instance <color=#58ed7d>created</color></b>");
        }
    }
    #endregion

    void Start()
    {
        PoolQueue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.name = objectPrefab.name + i.ToString();
            obj.transform.position -= Vector3.forward * (1f * i);
            obj.SetActive(false);
            PoolQueue.Enqueue(obj);
        }
        OnSpawnBomb(gameObject.transform.position);
    }

    public GameObject GetBomb(GameObject gameObject)
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
        PoolQueue.Enqueue(obj);
        OnSpawnBomb(gameObject.transform.position);
    }

    public void OnSpawnBomb(Vector3 spawnPosition)
    {
            bool spawned = false;
            int attempts = 0;

            while (!spawned && attempts < 10)
            {
                Vector3 randomPosition = spawnPosition + new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius));

                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPosition, out hit, spawnRadius, NavMesh.AllAreas))
                {
                    GetBomb(gameObject);
                    spawned = true;
                }
                else
                {
                    attempts++;
                }
            }

            if (!spawned)
            {
                Debug.LogError("Aucun point valide trouvé sur la NavMesh après plusieurs tentatives.");
            }
        }
}
