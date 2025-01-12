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

        // si conditions validées, change state

        /// si en sécurité, sans bombe -->  seek bomb state
        if(iaState.Behaviour.Bombs.Count == 0)
        {
            Debug.Log("bibo");
        }
        else
        {
            Debug.Log("une bombe");
        }
        /// si en sécurité, avec bombe -->  hunt state
        /// si vie = 0 --> death state
    }
}
