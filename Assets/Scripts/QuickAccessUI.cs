using UnityEngine;
using UnityEngine.UI;

public class QuickAccessUI : MonoBehaviour
{
    public Image[] slotImages; // ������ ��� �������� ������
    public Inventory inventory; // ������ �� ���������
    public Color activeColor = Color.white;  // ���� ��������� �����
    public Color inactiveColor = new Color(1, 1, 1, 0.5f); // ���� ���������� ������

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (i < inventory.items.Count && inventory.items[i] != null && inventory.items[i].weaponPrefab != null)
            {
                // ������������� ������ ������
                slotImages[i].sprite = inventory.items[i].weaponPrefab.GetComponent<SpriteRenderer>().sprite;
                slotImages[i].color = (i == inventory.ActiveSlot) ? activeColor : inactiveColor;
            }
            else
            {
                slotImages[i].sprite = null; // ���� ���� ������
                slotImages[i].color = new Color(1, 1, 1, 0); // ������ ��� ����������
            }
        }
    }
}
