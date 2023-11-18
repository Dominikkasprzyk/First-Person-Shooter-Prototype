using UnityEngine;

[CreateAssetMenu]
public class WeaponSO : ScriptableObject
{
    [field: SerializeField, Tooltip("True if this weapon deals continous damage over time (e.g. flame thrower, laser). " +
        "False if this weapon deals damage at intervals (e.g. bow, riffle, gun, axe, sword). ")] 
    public bool IsContinous { get; private set; }

    [field: SerializeField, Tooltip("True if this weapon supports full auto mode. False if the weapon should be released to fire after charging. ")] 
    public bool IsFullyAuto { get; private set; }

    [field: SerializeField, Min(0), Tooltip("Time the weapon has to be charged to reach max power. 0 if this weapon does not charge up.")] 
    public float MaxChargeUpTime { get; private set; }

    [field: SerializeField, Range(0, 1), Tooltip("Minimal percent of charging weapon up before it can be used.")] 
    public float MinChargePercent { get; private set; }

    [field: SerializeField, Min(0), Tooltip("Time before weapon can be used after last attack. ")] 
    private float timeBetweenAttacks;

    [field: SerializeField, Tooltip("Damage of one attack or, if this is continous weapon, damage per second.")]
    public float Damage { get; private set; } = 1;

    [field: SerializeField, Tooltip("Damage type.")]
    public Fabric DamageType { get; private set; }

    public WaitForSeconds CoolDownWait { get; private set; }

    private void OnEnable()
    {
        CoolDownWait = new WaitForSeconds(timeBetweenAttacks);
    }
}