using UnityEngine;
using System;

public class IASeekBombState : IABaseState
{
    public override void EnterState(IAStateManager iaState)
    {
        Reset();
    }

    public override void ExitState(IAStateManager iaState)
    {

    }

    public override void UpdateState(IAStateManager iaState)
    {        
        // si conditions valid�es, change state

        #region State Change Conditions
        // Run
        if (iaState.Behaviour.bombDetector.DetectedDangerousBombs.Count > 0)
        {
            iaState.SwitchState(iaState.IARunState);
        }

        // Hunt
        if (_hasToChange) iaState.SwitchState(iaState.IAHuntState);
        #endregion
        
        // comportement
        if (iaState.Behaviour.GetNearestBomb() != null)
        {
            iaState.Behaviour.Agent.destination = iaState.Behaviour.GetNearestBomb().transform.position;
        }
    }

    private static bool _hasToChange;
    private static bool _firstCheck;
    private static bool _scndCheck;

    public static void Check()
    {
        if (!_firstCheck)
        {
            _firstCheck = true;
            _hasToChange = (UnityEngine.Random.Range(0, 100) <= 40);
        }
        if (!_scndCheck)
        {
            _scndCheck = true;
            _hasToChange = (UnityEngine.Random.Range(0, 80) <= 80);
        }
        _hasToChange = true;
    }

    private void Reset()
    {
        _firstCheck = false;
        _scndCheck = false;
        _hasToChange = false;
    }
}
