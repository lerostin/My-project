using System.Collections.Generic;
using UnityEngine;
using TMPro; // Чтобы использовать красивый текст через TextMeshPro

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>(4);
    public WeaponSwitcher weaponSwitcher;
    public QuickAccessUI quickAccessUI;
    public int ActiveSlot { get; private set; } = 0;

    private bool canSwitch = true;
    private float switchCooldown = 1.5f;
    private float cooldownTimer = 0f;

    [Header("Cooldown Text UI")]
    public GameObject cooldownTextObject; // Ссылка на объект текста
    private TextMeshProUGUI cooldownText;  // Ссылка на сам текст

    void Start()
    {
        // Получаем компонент текста на старте
        if (cooldownTextObject != null)
            cooldownText = cooldownTextObject.GetComponent<TextMeshProUGUI>();

        // Скрываем текст при запуске
        if (cooldownText != null)
            cooldownTextObject.SetActive(false);
    }

    void Update()
    {
        if (!canSwitch)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownText != null)
            {
                cooldownText.text = $"{cooldownTimer:F1}"; // Показываем оставшееся время (1 знак после запятой)
            }

            if (cooldownTimer <= 0f)
            {
                canSwitch = true;
                if (cooldownTextObject != null)
                    cooldownTextObject.SetActive(false); // Скрыть текст после окончания отката
            }
        }

        if (canSwitch)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapon(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapon(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchWeapon(2);
            if (Input.GetKeyDown(KeyCode.Alpha4)) SwitchWeapon(3);
        }
    }

    void SwitchWeapon(int slot)
    {
        ActiveSlot = slot;

        bool hasWeapon = false;

        if (items[slot] != null && items[slot].weaponPrefab != null)
        {
            hasWeapon = true;
            weaponSwitcher.EquipWeapon(items[slot].weaponPrefab);

            var equipEffect = weaponSwitcher.GetComponent<WeaponEquipEffect>();
            if (equipEffect != null)
                equipEffect.PlayEquipAnimation();
        }

        FindAnyObjectByType<PlayerAttack>()?.SetHasWeapon(hasWeapon);
        quickAccessUI.UpdateUI();

        // Запускаем откат
        canSwitch = false;
        cooldownTimer = switchCooldown;

        // Показать текст отката
        if (cooldownTextObject != null)
            cooldownTextObject.SetActive(true);
    }
}
