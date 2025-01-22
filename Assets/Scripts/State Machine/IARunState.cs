using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class IARunState : IABaseState
{
    public override void EnterState(IAStateManager iaState)
    {
    }

    public override void ExitState(IAStateManager iaState)
    {
    }

    public override void UpdateState(IAStateManager iaState)
    {
        // Transitions
        if (iaState.Behaviour.bombDetector.DetectedDangerousBombs.Count <= 0)
        {
            if (iaState.Behaviour.Bombs.Count > 0)
            {
                // To Hunt state
                iaState.SwitchState(iaState.IAHuntState);
            }
            else
            {
                // To Seek Bomb state
                iaState.SwitchState(iaState.IASeekBombState);
            }
        }

        // behaviour
        Vector3 centerOfMass;
        float xSum = 0;
        float zSum = 0;

        for (int i = 0; i < iaState.Behaviour.bombDetector.DetectedDangerousBombs.Count; i++)
        {
            xSum += iaState.Behaviour.bombDetector.DetectedDangerousBombs[i].transform.position.x;
            zSum += iaState.Behaviour.bombDetector.DetectedDangerousBombs[i].transform.position.z;
        }

        xSum /= iaState.Behaviour.bombDetector.DetectedDangerousBombs.Count;
        zSum /= iaState.Behaviour.bombDetector.DetectedDangerousBombs.Count;

        centerOfMass = new Vector3(xSum, 0, zSum);
        iaState.Behaviour.AdditionalTarget = centerOfMass;

        Vector3 oppositeDir = -(centerOfMass.normalized - iaState.transform.position.normalized) * 5;

        Vector3 target = (Vector3.Distance(oppositeDir, iaState.Behaviour.transform.position) > 3) ? oppositeDir : oppositeDir + Vector3.right * UnityEngine.Random.Range(-4, 4);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(oppositeDir, out hit, 0.5f, NavMesh.AllAreas))
        {
            iaState.Behaviour.Agent.SetDestination(oppositeDir);
        }

    }
}
