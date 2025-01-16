using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
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
        // comportement
        Vector3 centerOfMass;
        float xSum = 0;
        float zSum = 0;

        if (iaState.Behaviour.bombDetector.DetectedDangerousBombs.Count <= 0)
        {
            for (int i = 0; i < iaState.Behaviour.bombDetector.DetectedDangerousBombs.Count; i++)
            {
                xSum += iaState.Behaviour.bombDetector.DetectedDangerousBombs[i].transform.position.x;
                zSum += iaState.Behaviour.bombDetector.DetectedDangerousBombs[i].transform.position.z;
            }

            xSum /= iaState.Behaviour.bombDetector.DetectedDangerousBombs.Count;
            zSum /= iaState.Behaviour.bombDetector.DetectedDangerousBombs.Count;

            centerOfMass = new Vector3(xSum, 0, zSum);

            iaState.Behaviour.Agent.SetDestination(-(centerOfMass.normalized - iaState.transform.position.normalized) * 6);
            // si conditions validées, change state
        }

        /// si en sécurité, sans bombe -->  seek bomb state
        if (iaState.Behaviour.DetectGameObjectByLayer(7) == null)
        {
            Debug.Log("SECURITE");
            if(iaState.Behaviour.Bombs.Count > 0)
            {
                iaState.SwitchState(iaState.IAHuntState);
            }
            else
            {
                iaState.SwitchState(iaState.IASeekBombState);
            }
        }
        /// si en sécurité, avec bombe -->  hunt state
        /// si vie = 0 --> death state
    }
}
