using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;

    public int damage = 10;
    public Transform attackPoint;
    public float weaponRange;
    public LayerMask playerLayer;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);

        if (hits.Length > 0)
        {
            hits[0].GetComponent<PLayerHealth>().ChangeHealth(-damage);
        }
    }

    // ����� �����
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"���� ������� ����: {amount}, ��������: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ��� ����� �������� �������� ������, ��������� ���� � �.�.
        Destroy(gameObject);
    }
}
