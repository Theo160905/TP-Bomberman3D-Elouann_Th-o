using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class IARunState : IABaseState
{
    public override void EnterState(IAStateManager iaState)
    {
        Debug.Log("switch to run state");
    }

    public override void ExitState(IAStateManager iaState)
    {
        Debug.Log("exit run state");
    }

    public override void UpdateState(IAStateManager iaState)
    {
        // comportement

        // si conditions validées, change state

        /// si en sécurité, sans bombe -->  seek bomb state
        if(iaState.Behaviour.Bomb == null)
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
