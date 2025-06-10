using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType { get; private set; }

    public int Level { get; private set; }
    public float moveSpeed = 2f;
    public float maxHP = 10f;
    public float currentHP;

    public float attackDamage = 5f;
    
    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        
        if (currentHP <= 0)
            Die();
    }

    private void Die()
    {
        GameManager.Instance.AddGold(10); // 죽으면 골드 획득 임의의 고정값
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
