using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABehaviour : MonoBehaviour
{
    // IA testing
    public float fov;
    public float maxRange;
    public float minRange;
    public float aspect;

    public GameObject Player;

    [field: SerializeField]
    public GameObject Bomb { get; private set; }

    public NavMeshAgent Agent;

    private void Update()
    {
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
        //Gizmos.DrawFrustum(this.transform.position, fov, maxRange, minRange, aspect);
        //Gizmos.DrawWireCube(this.transform.position + this.transform.forward * 2.6f, Vector3.one + this.transform.forward * 5);
        //Gizmos.DrawWireCube(this.transform.position + this.transform.right * 2.6f, Vector3.one + this.transform.right * 5);
        //Gizmos.DrawWireCube(this.transform.position - this.transform.right * 2.6f, Vector3.one - this.transform.right * 5);
        //Gizmos.DrawWireCube(this.transform.position - this.transform.forward * 2.6f, Vector3.one - this.transform.forward * 5);
        Gizmos.DrawRay(new Ray(this.transform.position, transform.forward));
        Gizmos.DrawRay(new Ray(this.transform.position, -transform.forward));
        Gizmos.DrawRay(new Ray(this.transform.position, transform.right));
        Gizmos.DrawRay(new Ray(this.transform.position, -transform.right));
    }
}
