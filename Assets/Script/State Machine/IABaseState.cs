using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IABaseState
{
    // enter the state
    public abstract void EnterState(IAStateManager iaState);

    // called every frames
    public abstract void UpdateState(IAStateManager iaState);

    // exit the state
    public abstract void ExitState(IAStateManager iaState);
}
