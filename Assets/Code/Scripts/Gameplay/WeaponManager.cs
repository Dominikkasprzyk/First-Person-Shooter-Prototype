using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<Weapon> availableWeapons;
    [SerializeField] private Transform weaponAnchorPoint;
    private int currentWeaponIndex;
    private List<Weapon> _spawnedWeapons;
    [SerializeField] private GameEvent _weaponChangedEvent;

    [Header("References")]
    [SerializeField] private InputManager inputManager;

    private void Awake()
    {
        currentWeaponIndex = 0;
    }

    private void Start()
    {
        _spawnedWeapons = new List<Weapon>();
        foreach(Weapon weapon in availableWeapons)
        {
            GameObject spawnedWeapon = Instantiate(weapon.gameObject, weaponAnchorPoint.position, weaponAnchorPoint.rotation);
            spawnedWeapon.transform.parent = weaponAnchorPoint;
            spawnedWeapon.SetActive(false);
            _spawnedWeapons.Add(spawnedWeapon.GetComponent<Weapon>());
        }
        ChangeWeapon();
    }

    private void OnEnable()
    {
        inputManager.changeWeaponEvent += OnChangeWeapon;
    }

    private void OnDisable()
    {
        inputManager.changeWeaponEvent -= OnChangeWeapon;
    }

    private void OnChangeWeapon(float _direction)
    {
        int _previousWeaponIndex = currentWeaponIndex;
        if (_direction > 0)
        {
            if(currentWeaponIndex + 1 < availableWeapons.Count)
            {
                currentWeaponIndex++;
            } else if(availableWeapons.Count > 1)
            {
                currentWeaponIndex = 0;
            }
        } else if(_direction <0)
        {
            if (currentWeaponIndex - 1 >= 0)
            {
                currentWeaponIndex--;
            }
            else if (availableWeapons.Count > 1)
            {
                currentWeaponIndex = availableWeapons.Count - 1;
            }
        }

        if (_previousWeaponIndex != currentWeaponIndex)
        {
            ChangeWeapon();
        }
    }

    private void ChangeWeapon()
    {
        foreach (Weapon weapon in _spawnedWeapons)
        {
            weapon.gameObject.SetActive(false);
        }
        _spawnedWeapons[currentWeaponIndex].gameObject.SetActive(true);
        _weaponChangedEvent.Raise(this, _spawnedWeapons[currentWeaponIndex].weaponBaseStats);
    }
}
