using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class IABehaviour : MonoBehaviour
{
    [Header("System")]
    public GameObject PlayerTarget;
    public NavMeshAgent Agent;
    public IABombDetectionRadius bombDetector;

    private Ray _sensorRay1;
    private Ray _sensorRay2;
    private Ray _sensorRay3;
    private Ray _sensorRay4;

    [Header("Detected objects")]
    public GameObject Detected1;
    public GameObject Detected2;
    public GameObject Detected3;
    public GameObject Detected4;

    [Header("Debug")]
    public Color TargetColor;
    [field: SerializeField]
    public Queue<GameObject> Bombs { get; private set; } = new();
    private bool _canUseBomb;

    private void Update()
    {
        // TEMPORAIRE
        // Si le navmesh agent atteint sa destination, il se stoppe
        if (Vector3.Distance(Agent.destination, this.transform.position) <= 1.5f)
        {
            if (Agent.isStopped) return;
        }
    }

    private void FixedUpdate()
    {
        _sensorRay1 = new Ray(this.transform.position, Vector3.forward * 3);
        _sensorRay2 = new Ray(this.transform.position, -Vector3.forward * 3);
        _sensorRay3 = new Ray(this.transform.position, -Vector3.right * 3);
        _sensorRay4 = new Ray(this.transform.position, Vector3.right * 3);

        if (Physics.SphereCast(_sensorRay1, 0.3f, out RaycastHit hit1))
        {
            Detected1 = hit1.collider.gameObject;
            if (Vector3.Distance(Detected1.transform.position, this.gameObject.transform.position) > 5.5f) return;
        }

        if (Physics.SphereCast(_sensorRay2, 0.3f, out RaycastHit hit2))
        {
            Detected2 = hit2.collider.gameObject;
            if (Vector3.Distance(Detected2.transform.position, this.gameObject.transform.position) > 5.5f) return;
        }

        if (Physics.SphereCast(_sensorRay3, 0.3f, out RaycastHit hit3))
        {
            Detected3 = hit3.collider.gameObject;
            if (Vector3.Distance(Detected3.transform.position, this.gameObject.transform.position) > 5.5f) return;
        }

        if (Physics.SphereCast(_sensorRay4, 0.3f, out RaycastHit hit4))
        {
            Detected4 = hit4.collider.gameObject;
            if (Vector3.Distance(Detected3.transform.position, this.gameObject.transform.position) > 5.5f) return;
        }
    }

    // Permet à l'IA de ramasser les bombes
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 && other.GetComponent<Bomb>().CanBeRecup)
        {
            other.GetComponent<Bomb>().CanBeRecup = false;
            for (int i = 0; i < Bombs.Count; i++)
            {
                if (Bombs.Peek() == null)
                {
                    Bombs.Enqueue(other.gameObject);
                    other.gameObject.SetActive(false);
                    Debug.Log(Bombs.Peek());
                    return;
                }
            }
        }
    }

    // Permet à l'IA de poser une bombe
    public void UseBomb()
    {
        if (Bombs.Count > 0)
        {
            if (_canUseBomb) return;
            StartCoroutine(Wait());
            GameObject bomb = Bombs.Dequeue();
            bomb.SetActive(true);
            bomb.transform.position = new Vector3(Mathf.RoundToInt(gameObject.transform.position.x), gameObject.transform.position.y - 0.5f, Mathf.RoundToInt(gameObject.transform.position.z));
            bomb.gameObject.GetComponent<Bomb>().ExplodeBomb();
            return;
        }
    }

    private IEnumerator Wait()
    {
        _canUseBomb = true;
        yield return new WaitForSeconds(3f);
        _canUseBomb = false;
    }

    public GameObject GetDetectedPlayer()
    {
        GameObject result = null;
        if (Detected1 != null)
        {
            if (Detected1.CompareTag("Player"))
            {
                result = Detected1;
                return result;
            }
        }
        if (Detected2 != null)
        {
            if (Detected2.CompareTag("Player"))
            {
                result = Detected2;
                return result;
            }
        }
        if (Detected3 != null)
        {
            if (Detected3.CompareTag("Player"))
            {
                result = Detected3;
                return result;
            }
        }
        if (Detected4 != null)
        {
            if (Detected4.CompareTag("Player"))
            {
                result = Detected4;
                return result;
            }
        }
        return result;
    }

    public GameObject GetNearestBomb()
    {
        GameObject nearestBomb = ObjectPoolBomb.Instance.PoolQueue.Peek();

        foreach (GameObject bomb in ObjectPoolBomb.Instance.PoolQueue)
        {
            if (Vector3.Distance(bomb.transform.position, this.transform.position) < Vector3.Distance(nearestBomb.transform.position, this.transform.position))
            {
                if (!Agent.CalculatePath(bomb.transform.position, Agent.path)) return nearestBomb;
                nearestBomb = bomb;
            }
        }

        return nearestBomb;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_sensorRay1);
        Gizmos.DrawRay(_sensorRay2);
        Gizmos.DrawRay(_sensorRay3);
        Gizmos.DrawRay(_sensorRay4);
        Gizmos.color = TargetColor;
        Gizmos.DrawSphere(Agent.destination, 0.5f);
    }
}
