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

    public int Gold { get; private set; }

    public int MoveSpeedLevel { get; private set; } = 0;
    public int AttackPowerLevel { get; private set; } = 0;
    public int AttackSpeedLevel { get; private set; } = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StateMachine = new PlayerStateMachine();
        StateMachine.AddState("Idle", new IdleState(this));
        StateMachine.AddState("Move", new MoveState(this));
        StateMachine.AddState("Attack", new AttackState(this));
        StateMachine.ChangeState("Idle");

        // 필요하면 저장된 데이터 불러오기
        Load();
        UIManager.Instance.UpdateGoldUI(Gold);
    }

    private void Update()
    {
        StateMachine.Update();
    }

    public void AddGold(int amount)
    {
        Gold += amount;
        UIManager.Instance.UpdateGoldUI(Gold);
    }

    public void SpendGold(int amount)
    {
        Gold -= amount;
        UIManager.Instance.UpdateGoldUI(Gold);
    }

    public bool HasEnoughGold(int amount) => Gold >= amount;

    public void UpgradeMoveSpeed()
    {
        moveSpeed += 0.5f;
        MoveSpeedLevel++;
    }

    public void UpgradeAttackPower()
    {
        attackPower += 1f;
        AttackPowerLevel++;
    }

    public void UpgradeAttackSpeed()
    {
        attackCooldown = Mathf.Max(0.2f, attackCooldown - 0.1f);
        AttackSpeedLevel++;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Gold", Gold);
        PlayerPrefs.SetInt("MoveSpeedLv", MoveSpeedLevel);
        PlayerPrefs.SetInt("AttackPowerLv", AttackPowerLevel);
        PlayerPrefs.SetInt("AttackSpeedLv", AttackSpeedLevel);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        Gold = PlayerPrefs.GetInt("Gold", 0);
        MoveSpeedLevel = PlayerPrefs.GetInt("MoveSpeedLv", 0);
        AttackPowerLevel = PlayerPrefs.GetInt("AttackPowerLv", 0);
        AttackSpeedLevel = PlayerPrefs.GetInt("AttackSpeedLv", 0);

        moveSpeed += 0.5f * MoveSpeedLevel;
        attackPower += 1f * AttackPowerLevel;
        attackCooldown = Mathf.Max(0.2f, attackCooldown - 0.1f * AttackSpeedLevel);
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
}
