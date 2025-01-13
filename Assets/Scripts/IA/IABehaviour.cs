using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABehaviour : MonoBehaviour
{
    [Header("References")]
    public GameObject Player;
    public NavMeshAgent Agent;
    public Color TargetColor;

    private Ray _sensorRay1;
    private Ray _sensorRay2;
    private Ray _sensorRay3;
    private Ray _sensorRay4;

    [Header("Detected objects")]
    public GameObject Detected1;
    public GameObject Detected2;
    public GameObject Detected3;
    public GameObject Detected4;

    [field: SerializeField]
    public Queue<GameObject> Bombs { get; private set; }


    private void Update()
    {
        // TEMPORAIRE
        // Si le navmesh agent atteint sa destination, il se stoppe
        if(Vector3.Distance(Agent.destination, this.transform.position) <= 1.5f)
        {
            if (Agent.isStopped) return;
            print("dest reached");
            Agent.isStopped = true;
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
        }

        if (Physics.SphereCast(_sensorRay2, 0.3f, out RaycastHit hit2))
        {
            Detected2 = hit2.collider.gameObject;
        }

        if (Physics.SphereCast(_sensorRay3, 0.3f, out RaycastHit hit3))
        {
            Detected3 = hit3.collider.gameObject;
        }

        if (Physics.SphereCast(_sensorRay4, 0.3f, out RaycastHit hit4))
        {
            Detected4 = hit4.collider.gameObject;
        }
    }

    public GameObject GetNearestBomb()
    {
        GameObject nearestBomb = BombPool.Instance.PoolQueue.Peek();

        foreach (GameObject bomb in BombPool.Instance.PoolQueue)
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
