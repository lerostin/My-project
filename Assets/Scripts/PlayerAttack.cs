using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int damage = 15;

    public float attackDelay = 0.15f; // Задержка до момента урона

    public bool canAttack = true;
    private bool isAttacking = false;
    public bool hasWeapon = false;

    public void SetAttackEnabled(bool value)
    {
        canAttack = value;
    }

    public void SetHasWeapon(bool value)
    {
        hasWeapon = value;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack && hasWeapon)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine()
    {
        isAttacking = true;

        // здесь можно включить анимацию, звук, визуальный эффект
        Debug.Log("Начинается атака...");

        yield return new WaitForSeconds(attackDelay); // ЗАДЕРЖКА ДО МОМЕНТА УРОНА

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(damage);
        }

        Debug.Log("Урон нанесён!");

        yield return new WaitForSeconds(0.2f); // задержка между атаками
        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
