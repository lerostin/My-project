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

    // Новый метод
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"Враг получил урон: {amount}, осталось: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Тут можно добавить анимацию смерти, выпадение лута и т.д.
        Destroy(gameObject);
    }
}
