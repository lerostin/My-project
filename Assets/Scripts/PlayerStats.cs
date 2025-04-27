using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Статы игрока")]
    public int maxHealthPlayer = 100;
    public int damage = 15;

    [Header("UI Элементы")]
    public Text healthText;
    public Text damageText;

    void Start()
    {
        UpdateStatsUI();
    }

    public void ChangeHealth(int amount)
    {
        maxHealthPlayer += amount;
        maxHealthPlayer = Mathf.Max(0, maxHealthPlayer);
        UpdateStatsUI();
    }

    public void ChangeDamage(int amount)
    {
        damage += amount;
        UpdateStatsUI();
    }

    public void UpdateStatsUI()
    {
        if (healthText != null)
            healthText.text = maxHealthPlayer.ToString();

        if (damageText != null)
            damageText.text = damage.ToString();
    }
}
