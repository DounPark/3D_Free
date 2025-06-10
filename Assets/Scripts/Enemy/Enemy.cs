using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType { get; private set; }
    private Rigidbody rb;

    public int Level { get; private set; }
    public float moveSpeed = 2f;
    public float maxHP = 10f;
    public float currentHP;

    public float attackDamage = 5f;
    
    Transform target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        currentHP = maxHP;
        target = Player.Instance.transform;
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            Vector3 nextPos = transform.position + dir * moveSpeed * Time.deltaTime;
            rb.MovePosition(nextPos);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        
        if (currentHP <= 0)
            Die();
    }

    private void Die()
    {
        int stage = StageManager.Instance.currentStage;
        int goldReward = 10 + (stage - 1) * 2; // 스테이지 1당 골드 +2 증가
        Player.Instance.AddGold(goldReward);

        //  (선택) 아이템 드랍 확률 예시
        float dropChance = 0.05f + 0.01f * (stage - 1);
        if (Random.value < dropChance)
        {
            Debug.Log("아이템 드랍!"); // 나중에 아이템 프리팹 생성하면 됨
        }

        Destroy(gameObject);
    }

    public void Init(int waveLevel, EnemyType type)
    {
        Level = waveLevel;
        enemyType = type;
        // 타입별 스탯 설정
        switch (type)
        {
            case EnemyType.Normal:
                maxHP = 10 + waveLevel * 2;
                moveSpeed = 2f;
                attackDamage = 5f;
                break;
            case EnemyType.Fast:
                maxHP = 6 + waveLevel * 1.5f;
                moveSpeed = 4f;
                attackDamage = 4f;
                break;
            case EnemyType.Tank:
                maxHP = 30 + waveLevel * 4;
                moveSpeed = 1.2f;
                attackDamage = 8f;
                break;
            case EnemyType.Ranged:
                maxHP = 8 + waveLevel * 2;
                moveSpeed = 2f;
                attackDamage = 6f;
                break;
            case EnemyType.Boss:
                maxHP = 100 + waveLevel * 10;
                moveSpeed = 1.5f;
                attackDamage = 15f;
                break;
        }
        
        currentHP = maxHP;
    }
}
