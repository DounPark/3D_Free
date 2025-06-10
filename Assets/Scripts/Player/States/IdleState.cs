using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private Player _player;

    public IdleState(Player player)
    {
        _player = player;
    }
    
    public void Enter()
    {
        // 애니메이션이나 효과 초기화
    }

    public void Execute()
    {
        // 주변에 적이 있으면 상태 전환
        if (_player.HasTarget())
            _player.StateMachine.ChangeState("Move");
        if (!_player.HasTarget())
        {
            _player.StateMachine.ChangeState("Move"); //  상태 전환이 꼭 있어야 함
        }
    }

    public void Exit()
    {
        
    }
}
