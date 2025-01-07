using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABehaviour : MonoBehaviour
{
    public Transform target;

    //[field: SerializeField]
    public GameObject Bomb { get; private set; }

    public NavMeshAgent agent;

    private void Update()
    {
        agent.destination = target.position;
    }

}
