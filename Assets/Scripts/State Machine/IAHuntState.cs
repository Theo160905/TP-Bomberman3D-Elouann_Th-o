using UnityEngine;
using UnityEngine.Scripting;

public class IAHuntState : IABaseState
{
    public override void EnterState(IAStateManager iaState)
    {
        iaState.Behaviour.Agent.isStopped = false;
    }

    public override void ExitState(IAStateManager iaState)
    {
        iaState.Behaviour.Agent.isStopped = true;
    }

    public override void UpdateState(IAStateManager iaState)
    {
        #region Transitions
        // Run
        if (iaState.Behaviour.bombDetector.DetectedDangerousBombs.Count > 0)
        {
            iaState.SwitchState(iaState.IARunState);
        }

        // Seek bomb
        if (iaState.Behaviour.Bombs.Count <= 0)
        {
            iaState.SwitchState(iaState.IASeekBombState);
        }
        #endregion

        // comportement
        Vector3 targetPos = new Vector3(iaState.Behaviour.PlayerTarget.transform.position.x, iaState.transform.position.y, iaState.Behaviour.PlayerTarget.transform.position.z);
        iaState.Behaviour.Agent.destination = targetPos;

        if (iaState.Behaviour.DetectGameObjectByLayer(3) == null) return;
        if (Vector3.Distance(iaState.Behaviour.DetectGameObjectByLayer(3).transform.position, iaState.transform.position) <= 4)
        {
            iaState.Behaviour.UseBomb();
        }
    }
}
