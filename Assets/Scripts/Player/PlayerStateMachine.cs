using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    private Dictionary<string, IState> _states = new();
    private IState _currentState;

    public void AddState(string key, IState state)
    {
        _states[key] = state;
    }

    public void ChangeState(string key)
    {
        if(_currentState != null)
            _currentState.Exit();
        
        _currentState = _states[key];
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState?.Execute();
    }
}
