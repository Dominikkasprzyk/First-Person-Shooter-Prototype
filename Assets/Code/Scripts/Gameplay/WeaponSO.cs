using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Weapon SO")]
public class WeaponSO : ScriptableObject
{
    [field: SerializeField, Tooltip("True if this weapon supports full auto mode. False if the weapon should be released to fire after charging. ")] 
    public bool IsFullyAuto { get; private set; }

    [field: SerializeField, Min(0), Tooltip("Time the weapon has to be charged to reach max power. 0 if this weapon does not charge up.")] 
    public float MaxChargeUpTime { get; private set; }

    [field: SerializeField, Range(0, 1), Tooltip("Minimal percent of charging weapon up before it can be used.")] 
    public float MinChargePercent { get; private set; }

    [field: SerializeField, Min(0), Tooltip("Time before weapon can be used after last attack. ")] 
    public float TimeBetweenAttacks { get; private set; }

    [field: SerializeField, Tooltip("Damage of one attack or, if this is continous weapon, damage per second.")]
    public float Damage { get; private set; } = 1;

    [field: SerializeField, Tooltip("Damage type.")]
    public Fabric DamageType { get; private set; }

    [field: SerializeField, Min(0), Tooltip("Time the weapon has to be prepared for when drawn.")]
    public float PreparationTime { get; private set; }
}