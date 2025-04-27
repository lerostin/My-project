using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject inventoryUI; // ���� ������������� ������

    private bool isOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            inventoryUI.SetActive(isOpen);
        }
    }
}
