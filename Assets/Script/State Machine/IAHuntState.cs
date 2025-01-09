using UnityEngine;

public class IAHuntState : IABaseState
{
    public override void EnterState(IAStateManager iaState)
    {
        Debug.Log("switch to hunt state");
    }

    public override void ExitState(IAStateManager iaState)
    {
        Debug.Log("exit hunt state");
    }

    public override void UpdateState(IAStateManager iaState)
    {
        // comportement
        iaState.Behaviour.agent.destination = iaState.Behaviour.Player.transform.position;
        Debug.Log($"target pos : { iaState.Behaviour.Player.transform.position} | current targeted pos : {iaState.Behaviour.agent.destination}");

        if (iaState.transform.position == iaState.Behaviour.agent.destination) Debug.Log("nickel"); 
        // si conditions valid�es, change state

        /// si bombe g�ch�e --> seek bomb state
        /// si bombe dangereuse pos�e --> run state
        /// si vie = 0 --> death state
    }
}
