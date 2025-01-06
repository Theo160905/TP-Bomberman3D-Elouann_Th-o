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

        // si conditions valid�es, change state

        /// si bombe g�ch�e --> seek bomb state
        /// si bombe dangereuse pos�e --> run state
        /// si vie = 0 --> death state
    }
}
