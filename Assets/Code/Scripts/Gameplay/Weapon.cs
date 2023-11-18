using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    [field: SerializeField] public WeaponSO weaponBaseStats { get; private set; }
    [Min(0f)]

    private float _chargePercent = 0f;
    private Coroutine _chargingRoutine;

    [Header("References")]
    [SerializeField] private InputManager inputManager;

    [Header("Events")]
    public GameEvent weaponFiredEvent;

    protected Transform playerCameraTransform;

    private void OnEnable()
    {
        inputManager.attackReleaseEvent += PerformAttack;
        inputManager.attackChargeEvent += ChargeUpAttack;
    }

    private void OnDisable()
    {
        inputManager.attackReleaseEvent -= PerformAttack;
        inputManager.attackChargeEvent -= ChargeUpAttack;
    }

    private void Awake()
    {
        playerCameraTransform = Camera.main.transform;    
    }

    public virtual void PerformAttack()
    {
        if(_chargingRoutine != null)
            StopCoroutine(_chargingRoutine);
        if (weaponBaseStats.MinChargePercent <= _chargePercent)
        {
            weaponFiredEvent.Raise(this, null);
        }
        _chargePercent = 0;
    }

    public virtual void ChargeUpAttack()
    {
        _chargingRoutine = StartCoroutine(ChargeWeaponUp());
    }

    private IEnumerator ChargeWeaponUp()
    {
        float currentChargingTime = 0f;
        float singleTickDuration = 0.1f;
        while (currentChargingTime < weaponBaseStats.MaxChargeUpTime)
        {
            yield return new WaitForSeconds(singleTickDuration);
            {
                currentChargingTime += singleTickDuration;
                _chargePercent += singleTickDuration / weaponBaseStats.MaxChargeUpTime;
            }
        }
        if(_chargePercent > 1)
        {
            _chargePercent = 1;
        }
    }
}
