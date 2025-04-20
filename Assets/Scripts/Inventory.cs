using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>(4);
    public WeaponSwitcher weaponSwitcher;
    public QuickAccessUI quickAccessUI; // —сылка на UI
    public int ActiveSlot { get; private set; } = 0; // јктивный слот

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SwitchWeapon(3);
    }

    void SwitchWeapon(int slot)
    {
        ActiveSlot = slot;

        bool hasWeapon = false;

        if (items[slot] != null && items[slot].weaponPrefab != null)
        {
            hasWeapon = true;
            weaponSwitcher.EquipWeapon(items[slot].weaponPrefab);

            // Ёффект доставани€
            var equipEffect = weaponSwitcher.GetComponent<WeaponEquipEffect>();
            if (equipEffect != null)
                equipEffect.PlayEquipAnimation();
        }

        // ”станавливаем флаг в PlayerAttack
        FindAnyObjectByType<PlayerAttack>()?.SetHasWeapon(hasWeapon);

        quickAccessUI.UpdateUI();
    }
}
