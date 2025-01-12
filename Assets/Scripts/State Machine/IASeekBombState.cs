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

        // si conditions valid�es, change state

        #region State Change Conditions
        // Hunt
        if (iaState.Behaviour.Bombs.Count != 0)
        {
            iaState.SwitchState(iaState.IAHuntState);
        }

        // Run
        #endregion

        /// si bombe trouv�e --> hunt state
        /// si bombe dangereuse pos�e --> run state
        /// si vie = 0 --> death state
    }
}
