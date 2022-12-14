using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyฬUฬฎ
/// </summary>
public class EnemyAttack : MonoBehaviour
{
    [Header("GUฬvnu")]
    [SerializeField, Tooltip("GUฬvnu")] GameObject _enemyAttackPrefab;
    [SerializeField, Tooltip("ฝbจซษUท้ฉ")] float _atkInterval = 2f;
    float _timer = 0f;

    private void Update()
    {
        _timer += Time.deltaTime;

        if( _timer > _atkInterval)
        {
            Attack();
            _timer = 0f;
        }
    }

    private void Attack()
    {
        Instantiate(_enemyAttackPrefab, this.transform);
        Debug.Log("GฬU");
    }
}
