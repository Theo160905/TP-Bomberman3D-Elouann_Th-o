using UnityEngine;

public class IASeekBombState : IABaseState
{
    public override void EnterState(IAStateManager iaState)
    {
        Debug.Log("switch to bomb seek state");
    }

    public override void ExitState(IAStateManager iaState)
    {
        Debug.Log("exit bomb seek state");
    }

    public override void UpdateState(IAStateManager iaState)
    {
        // comportement

        // si conditions validées, change state

        /// si bombe trouvée --> hunt state
        /// si bombe dangereuse posée --> run state
        /// si vie = 0 --> death state
    }
}
