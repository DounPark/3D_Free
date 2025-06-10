using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private Player _player;
    private Transform _target;
    private float _attackCooldown = 1f;
    private float _lastAttackTime = 0f;

    public AttackState(Player player)
    {
        _player = player;
    }
    
    public void Enter()
    {
        _target = _player.FindClosestEnemy();
    }

    public void Execute()
    {
        if (_target == null)
        {
            _player.StateMachine.ChangeState("Idle");
            return;
        }
        
        // 타겟 사망 확인
        Enemy enemy = _target.GetComponent<Enemy>();
        if (enemy == null)
        {
            _player.StateMachine.ChangeState("Idle");
            return;
        }
        
        // 공격 쿨타임 체크
        if (Time.time - _lastAttackTime >= _attackCooldown)
        {
            _lastAttackTime = Time.time;
            // enemy.TakeDamage(5f); // 임시 고정값
            enemy.TakeDamage(_player.attackPower);
            
            Debug.Log($"Attacked : {_target.name}");
        }
        
        // 타겟과 거리가 멀어졌으면 move로
        float dist = Vector3.Distance(_player.transform.position, _target.position);
        if (dist > 1.5f)
        {
            _player.StateMachine.ChangeState("Move");
        }
    }

    public void Exit()
    {
        
    }
}
