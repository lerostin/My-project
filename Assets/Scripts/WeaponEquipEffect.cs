using UnityEngine;

public class WeaponEquipEffect : MonoBehaviour
{
    public Transform weaponHolder;
    public Vector3 hiddenOffset = new Vector3(0f, -0.5f, 0f);
    public float drawDuration = 0.3f;

    private Vector3 shownPosition;
    private Vector3 hiddenPosition;
    private float timer;
    private bool isDrawing;

    private PlayerAttack playerAttack;

    void Start()
    {
        playerAttack = FindAnyObjectByType<PlayerAttack>();
    }

    public void PlayEquipAnimation()
    {
        shownPosition = weaponHolder.localPosition;
        hiddenPosition = shownPosition + hiddenOffset;

        weaponHolder.localPosition = hiddenPosition;
        timer = 0f;
        isDrawing = true;

        // Блокируем атаку на время доставания
        if (playerAttack != null)
            playerAttack.SetAttackEnabled(false);
    }

    void Update()
    {
        if (!isDrawing) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / drawDuration);
        weaponHolder.localPosition = Vector3.Lerp(hiddenPosition, shownPosition, t);

        if (t >= 1f)
        {
            isDrawing = false;

            // Вновь разрешаем атаку
            if (playerAttack != null)
                playerAttack.SetAttackEnabled(true);
        }
    }
}
