using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        // behaviour
        Vector3 centerOfMass;
        float xSum = 0;
        float zSum = 0;

        if (iaState.Behaviour.bombDetector.DetectedDangerousBombs.Count > 0)
        {
            for (int i = 0; i < iaState.Behaviour.bombDetector.DetectedDangerousBombs.Count; i++)
            {
                Debug.Log(iaState.Behaviour.bombDetector.DetectedDangerousBombs[i].name);
                xSum += iaState.Behaviour.bombDetector.DetectedDangerousBombs[i].transform.position.x;
                zSum += iaState.Behaviour.bombDetector.DetectedDangerousBombs[i].transform.position.z;
            }

            xSum /= iaState.Behaviour.bombDetector.DetectedDangerousBombs.Count;
            zSum /= iaState.Behaviour.bombDetector.DetectedDangerousBombs.Count;

            centerOfMass = new Vector3(xSum, 0, zSum);
            iaState.Behaviour.AdditionalTarget = centerOfMass;

            Vector3 oppositeDir = -(centerOfMass.normalized - iaState.transform.position.normalized) * 6;

            Vector3 target = (Vector3.Distance(oppositeDir, iaState.Behaviour.transform.position) > 3) ? oppositeDir : centerOfMass - iaState.transform.position;

            //iaState.Behaviour.Agent.SetDestination(-(centerOfMass.normalized - iaState.transform.position.normalized) * 6);
        }

        // Transitions
        if (iaState.Behaviour.bombDetector.DetectedDangerousBombs.Count <= 0)
        {
            if(iaState.Behaviour.Bombs.Count > 0)
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
    }
}
