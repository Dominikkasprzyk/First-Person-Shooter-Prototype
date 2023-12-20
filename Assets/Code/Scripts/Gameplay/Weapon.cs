using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    [field: SerializeField] public WeaponSO weaponBaseStats { get; private set; }

    private float _chargePercent = 0f;
    private Coroutine _chargingRoutine;
    private bool _canFire = false;
    private bool _holdingCharge = false;

    [Header("References")]
    [SerializeField] private InputManager _inputManager;

    [Header("Events")]
    [SerializeField] private GameEvent _weaponFiredEvent;
    [SerializeField] private GameEvent _weaponChargingEvent;
    [SerializeField] protected GameEvent _weaponHitEvent;

    private void OnEnable()
    {
        _inputManager.attackReleaseEvent += OnAttackRelease;
        _inputManager.attackChargeEvent += OnAttackCharge;
        StartCoroutine(WaitBeforeAttack(weaponBaseStats.PreparationTime));
    }

    private void OnDisable()
    {
        _inputManager.attackReleaseEvent -= OnAttackRelease;
        _inputManager.attackChargeEvent -= OnAttackCharge;
    }

    private void OnAttackRelease()
    {
        _holdingCharge = false;
        if (weaponBaseStats.MaxChargeUpTime > 0)
        {
            if (_chargePercent >= weaponBaseStats.MinChargePercent && _canFire)
            {
                PerformAttack();
            }
            if (_chargingRoutine != null)
            {
                StopCoroutine(_chargingRoutine);
                _chargePercent = 0;
                _weaponChargingEvent.Raise(this, false);
            } 
        }
    }

    abstract protected void Fire();

    private void OnAttackCharge()
    {
        _holdingCharge = true;
        if (weaponBaseStats.MaxChargeUpTime <= 0 && _canFire)
        {
            PerformAttack();
        }
        else
        {
            if (_canFire)
            {
                _chargingRoutine = StartCoroutine(ChargeWeaponUp());
                _weaponChargingEvent.Raise(this, true);
            } 
        }
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
        if(weaponBaseStats.IsFullyAuto)
        {
            PerformAttack();
        }
    }

    private IEnumerator WaitBeforeAttack(float timeToWait)
    {
        _canFire = false;
        yield return new WaitForSeconds(timeToWait);
        _canFire = true;
        if(_holdingCharge && (weaponBaseStats.IsFullyAuto || weaponBaseStats.MaxChargeUpTime > 0))
        {
            OnAttackCharge();
        }
    }

    private void PerformAttack()
    {
        _weaponFiredEvent.Raise(this, null);
        Fire();
        StartCoroutine(WaitBeforeAttack(weaponBaseStats.TimeBetweenAttacks));
        _chargePercent = 0f;
    }
}
