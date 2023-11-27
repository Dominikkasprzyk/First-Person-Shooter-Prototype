using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitscanBased : Weapon
{
    [SerializeField] private float _range = 50f;

    protected override void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, _range))
        {
            if (hit.collider.GetComponent<Damageable>() != null)
            {
                hit.collider.GetComponent<Damageable>().TakeHit(weaponBaseStats.Damage, weaponBaseStats.DamageType);
            }
        }
    }
}
