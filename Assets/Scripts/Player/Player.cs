using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public PlayerStateMachine StateMachine { get; private set; }

    public float moveSpeed = 3f;
    public float attackPower = 5f;
    public float attackCooldown = 1f;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StateMachine = new PlayerStateMachine();
        StateMachine.AddState("Idle", new IdleState(this));
        StateMachine.AddState("Move", new MoveState(this));
        StateMachine.AddState("Attack", new AttackState(this));
        
        StateMachine.ChangeState("Idle");
    }
    
    void Update()
    {
        StateMachine.Update();    
    }

    public bool HasTarget()
    {
        // 구현할 적 탐지
        return FindClosestEnemy() != null;
    }

    public Transform GetTarget()
    {
        // 앞으로 구현할 대상 반환
        return null;
    }

    public void MoveTo(Vector3 position)
    {
        
    }

    public void Attack()
    {
        
    }

    public Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(currentPos, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }
        
        return closest;
    }

    public void UpgradeMoveSpeed() => moveSpeed += 0.5f;
    public void UpgradeAttackPower() => attackPower += 1f;
    public void UpgradeAttackSpeed() => attackCooldown = Mathf.Max(0.2f, attackCooldown - 0.1f);
}
