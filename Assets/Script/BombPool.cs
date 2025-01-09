using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombPool : MonoBehaviour
{
    public GameObject objectPrefab;
    public int poolSize = 10;

    public Queue<GameObject> PoolQueue { get; private set; }

    // Singleton
    #region Singleton
    private static BombPool _instance;

    public static BombPool Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Bomb Pool");
                _instance = go.AddComponent<BombPool>();
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
            obj.SetActive(false);
            PoolQueue.Enqueue(obj);
        }
        OnSpawnBomb();
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
        OnSpawnBomb();
    }

    public void OnSpawnBomb()
    {

    }
}
