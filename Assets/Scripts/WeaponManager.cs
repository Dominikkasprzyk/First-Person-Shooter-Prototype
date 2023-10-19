using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> availableWeapons;
    [SerializeField] private Transform weaponAnchorPoint;
    private int currentWeaponIndex;
    
    [Header("References")]
    [SerializeField] private InputManager inputManager;

    private void Awake()
    {
        currentWeaponIndex = 0;
    }

    private void Start()
    {
        SpawnWeapon();
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
            SpawnWeapon();
        }
    }

    private void SpawnWeapon()
    {
        foreach (Transform child in weaponAnchorPoint)
        {
            Destroy(child.gameObject);
        }
        GameObject _newWeapon = Instantiate(availableWeapons[currentWeaponIndex], weaponAnchorPoint.position, weaponAnchorPoint.rotation);
        _newWeapon.transform.parent = weaponAnchorPoint;
    }
}
