using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveState : IState
{
    private Player _player;
    private NavMeshAgent _agent;
    private Transform _target;

    private float patrolDelay = 2f;
    private float patrolTimer = 0f;
    private bool isPatrolling = false;

    public MoveState(Player player)
    {
        _player = player;
        _agent = player.GetComponent<NavMeshAgent>();
    }

    public void Enter()
    {
        _target = _player.FindClosestEnemy();

        if (_target != null)
        {
            isPatrolling = false;
            _agent.SetDestination(_target.position);
        }
        else
        {
            isPatrolling = true;
            patrolTimer = 0f;
            SetRandomPatrolDestination();
        }
    }

    public void Execute()
    {
        if (!isPatrolling)
        {
            // 적 추적 중
            _target = _player.FindClosestEnemy();
            if (_target == null)
            {
                // 적이 없어졌으므로 순찰 시작
                isPatrolling = true;
                patrolTimer = 0f;
                SetRandomPatrolDestination();
                return;
            }

            float distance = Vector3.Distance(_player.transform.position, _target.position);
            if (distance < 1.5f)
            {
                _agent.isStopped = true;
                _player.StateMachine.ChangeState("Attack");
            }
            else
            {
                _agent.SetDestination(_target.position); // 계속 추적
            }
        }
        else
        {
            // 순찰 중
            if (!_agent.pathPending && _agent.remainingDistance < 0.3f)
            {
                patrolTimer += Time.deltaTime;
                if (patrolTimer >= patrolDelay)
                {
                    patrolTimer = 0f;
                    SetRandomPatrolDestination();
                }
            }

            // 중간에 적 발견 시 추적 모드로 전환
            _target = _player.FindClosestEnemy();
            if (_target != null)
            {
                isPatrolling = false;
                _agent.SetDestination(_target.position);
            }
        }
    }

    public void Exit()
    {
        _agent.isStopped = false;
    }

    private void SetRandomPatrolDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 5f;
        randomDirection += _player.transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 200f, NavMesh.AllAreas))
        {
            _agent.SetDestination(hit.position);
            Debug.Log("[MoveState] 순찰 목적지 설정: " + hit.position);
        }
        else
        {
            Debug.LogWarning("[MoveState] 순찰 위치 못 찾음");
        }
    }
}
