using UnityEngine;

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
        // si conditions valid�es, change state
        if (iaState.Behaviour.Bombs.Count <= 0)
        {
            //iaState.SwitchState(iaState.IASeekBombState);
        }

        // comportement
        Vector3 targetPos = new Vector3(iaState.Behaviour.PlayerTarget.transform.position.x, iaState.transform.position.y, iaState.Behaviour.PlayerTarget.transform.position.z);
        iaState.Behaviour.Agent.destination = targetPos;

        /// si bombe g�ch�e --> seek bomb state
        /// si bombe dangereuse pos�e --> run state
        /// si vie = 0 --> death state
    }
}
