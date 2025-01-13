using UnityEngine;

public class IAHuntState : IABaseState
{
    public override void EnterState(IAStateManager iaState)
    {
    }

    public override void ExitState(IAStateManager iaState)
    {
        iaState.Behaviour.Agent.isStopped = true;
    }

    public override void UpdateState(IAStateManager iaState)
    {
        // comportement
        Vector3 targetPos = new Vector3(iaState.Behaviour.Player.transform.position.x, iaState.transform.position.y, iaState.Behaviour.Player.transform.position.z);
        iaState.Behaviour.Agent.destination = targetPos;

        // si conditions validées, change state
        if (iaState.Behaviour.Bombs.Count <= 0)
        {
            iaState.SwitchState(iaState.IASeekBombState);
        }


        /// si bombe gâchée --> seek bomb state
        /// si bombe dangereuse posée --> run state
        /// si vie = 0 --> death state
    }
}
