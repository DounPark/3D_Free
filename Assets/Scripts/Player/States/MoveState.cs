using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveState : IState
{
    private Player _player;
    private NavMeshAgent _agent;
    private Transform _target;

    public MoveState(Player player)
    {
        _player = player;
        _agent = player.GetComponent<NavMeshAgent>();
    }
    
    public void Enter()
    {
        _target = _player.FindClosestEnemy();
        if(_target != null)
            _agent.SetDestination(_target.position);
    }

    public void Execute()
    {
        if (_target == null)
        {
            _player.StateMachine.ChangeState("Idle");
            return;
        }
        
        float distance = Vector3.Distance(_player.transform.position, _target.position);
        if (distance < 1.5f) // 공격 거리 진입 시
        {
            _agent.isStopped = true;
            _player.StateMachine.ChangeState("Attack");
        }
    }

    public void Exit()
    {
        _agent.isStopped = false;
    }
}
