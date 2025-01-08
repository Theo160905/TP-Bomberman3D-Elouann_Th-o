using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IADeathState : IABaseState
{
    public override void EnterState(IAStateManager iaState)
    {
        Debug.Log("switch to death state");
    }

    public override void ExitState(IAStateManager iaState)
    {
        Debug.Log("exit death state");
    }

    public override void UpdateState(IAStateManager iaState)
    {
        // comportement

        // FIN ?
    }
}
