using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject gravityGun;
    [SerializeField] private GameObject machineGun;

    private enum WeaponType { Gravity, Machine }
    private WeaponType _currentWeapon = WeaponType.Gravity;

    void Start()
    {
        SetWeapon(_currentWeapon);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _currentWeapon = _currentWeapon == WeaponType.Gravity ? WeaponType.Machine : WeaponType.Gravity;
            SetWeapon(_currentWeapon);
        }
    }

    private void SetWeapon(WeaponType weapon)
    {
        gravityGun.SetActive(weapon == WeaponType.Gravity);
        machineGun.SetActive(weapon == WeaponType.Machine);
    }
}