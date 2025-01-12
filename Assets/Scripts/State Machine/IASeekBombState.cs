using UnityEngine;
using System;

public class IASeekBombState : IABaseState
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

        // si conditions validées, change state

        #region State Change Conditions
        // Hunt
        if (iaState.Behaviour.Bombs.Count != 0)
        {
            iaState.SwitchState(iaState.IAHuntState);
        }

        // Run
        #endregion

        /// si bombe trouvée --> hunt state
        /// si bombe dangereuse posée --> run state
        /// si vie = 0 --> death state
    }
}
