using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitscanBased : Weapon
{
    [SerializeField] private float range = 50f;

    public override void PerformAttack()
    {
        base.PerformAttack();
        RaycastHit hit;
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, range))
        {
            if (hit.collider.GetComponent<Damageable>() != null)
            {
                hit.collider.GetComponent<Damageable>().TakeHit(weaponBaseStats.Damage, weaponBaseStats.DamageType);
            }
        }
    }
}
