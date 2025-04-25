using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    private GameObject currentWeapon;
    public Transform weaponHolder;

    public float swingAngle = 90f;
    public float forwardSpeed = 720f;  // Быстрое движение вперёд
    public float returnSpeed = 180f;   // Медленное возвращение назад

    private enum State { Idle, SwingingForward, Returning }
    private State state = State.Idle;

    private float currentAngle = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && state == State.Idle)
        {
            state = State.SwingingForward;
            currentAngle = 0f;
        }

        switch (state)
        {
            case State.SwingingForward:
                currentAngle += forwardSpeed * Time.deltaTime;

                if (currentAngle >= swingAngle)
                {
                    currentAngle = swingAngle;
                    state = State.Returning;
                }

                weaponHolder.localRotation = Quaternion.Euler(0, 0, -currentAngle);
                break;

            case State.Returning:
                currentAngle -= returnSpeed * Time.deltaTime;

                if (currentAngle <= 0f)
                {
                    currentAngle = 0f;
                    state = State.Idle;
                }

                weaponHolder.localRotation = Quaternion.Euler(0, 0, -currentAngle);
                break;
        }
    }
    public void EquipWeapon(GameObject weaponPrefab)
    {
        foreach (Transform child in weaponHolder)
        {
            Destroy(child.gameObject);
        }

        GameObject newWeapon = Instantiate(weaponPrefab, weaponHolder);
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localRotation = Quaternion.identity;
    }
}
