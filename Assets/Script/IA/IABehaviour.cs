using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABehaviour : MonoBehaviour
{
    // IA testing

    public Transform target;
    public GameObject BombGizmo;
    public GameObject WallGizmo;

    public GameObject Player;

    //[field: SerializeField]
    public GameObject Bomb { get; private set; }

    public NavMeshAgent agent;

    private void Update()
    {
        //agent.destination = target.position;
    }

    public void GetNearestBomb()
    {

    }
}
