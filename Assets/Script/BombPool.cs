using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BombPool : MonoBehaviour
{
    public GameObject objectPrefab;
    public int poolSize = 10;

    private Queue<GameObject> poolQueue;

    

    void Start()
    {
        poolQueue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
        OnSpawnBomb();
    }

    public GameObject GetBomb(GameObject gameObject)
    {
        if (poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
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
        poolQueue.Enqueue(obj);
        OnSpawnBomb();
    }

    public void OnSpawnBomb()
    {

    }
}
