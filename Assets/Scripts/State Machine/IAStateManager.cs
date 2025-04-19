using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAStateManager : MonoBehaviour
{
    // System
    public IABehaviour Behaviour { get; private set; }
    public Color SelectedColor;
    public Color UnselectedColor;

    // States
    public StateEnum CurrentStateDisplayed;

    private IABaseState _currentState;
    
    public IAHuntState IAHuntState = new();
    public IASeekBombState IASeekBombState = new();
    public IARunState IARunState = new();
    public IADeathState IADeathState = new();

    private void Awake()
    {
        Behaviour = TryGetComponent(out IABehaviour behaviour) ? behaviour : null;
    }

    private void Start()
    {
        // enter in the first state
        _currentState = IASeekBombState;
        _currentState.EnterState(this);
        UpdateCurrentStateEnum();
    }

    private void Update() // TEMPORARY
    {
        // use the update of the state
        _currentState.UpdateState(this);
    }

    public void SwitchState(IABaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
        print("switched to " + _currentState);
        UpdateCurrentStateEnum();
    }

    public void UpdateCurrentStateEnum()
    {
        switch (_currentState)
        {
            case IARunState state:
                CurrentStateDisplayed = StateEnum.Run;
                break;

            case IASeekBombState state:
                CurrentStateDisplayed = StateEnum.SeekBomb;
                break;

            case IAHuntState state:
                CurrentStateDisplayed = StateEnum.Hunt;
                break;

            default:
                CurrentStateDisplayed = StateEnum.Dead;
                break;
        }
    }
}
