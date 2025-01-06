using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAStateManager : MonoBehaviour
{
    // System
    public IABehaviour Behaviour { get; private set; }

    // States
    private IABaseState _currentState;
    
    private IAHuntState _iaHuntState = new();
    private IASeekBombState _iaSeekBombState = new();
    private IARunState _iaRunState = new();
    private IADeathState _iaDeathState = new();

    private void Awake()
    {
        Behaviour = TryGetComponent(out IABehaviour behaviour) ? behaviour : null;
        print(Behaviour);
    }

    private void Start()
    {
        // enter in the first state
        _currentState = _iaHuntState;
        _currentState.EnterState(this);
    }

    private void Update()
    {
        // use the update of the state
        _currentState.UpdateState(this);

        if (Input.GetKeyDown(KeyCode.H))
        {
            SwitchState(_iaHuntState);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SwitchState(_iaRunState);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SwitchState(_iaSeekBombState);
        }
    }

    public void SwitchState(IABaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }
}
