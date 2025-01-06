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

        // si conditions validées, change state

        /// si bombe gâchée --> seek bomb state
        /// si bombe dangereuse posée --> run state
        /// si vie = 0 --> death state
    }
}
