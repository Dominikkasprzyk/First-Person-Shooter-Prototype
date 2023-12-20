using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitscanBased : Weapon
{
    [SerializeField] private float _range = 50f;
    private Transform _playerCameraTransform;
    
    private void Awake()
    {
        _playerCameraTransform = Camera.main.transform;
    }

    protected override void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(_playerCameraTransform.position, _playerCameraTransform.forward, out hit, _range))
        {
            _weaponHitEvent.Raise(this, hit.point);
            if (hit.collider.GetComponent<Damageable>() != null)
            {
                hit.collider.GetComponent<Damageable>().TakeHit(weaponBaseStats.Damage, weaponBaseStats.DamageType);
            }
        }
    }
}
