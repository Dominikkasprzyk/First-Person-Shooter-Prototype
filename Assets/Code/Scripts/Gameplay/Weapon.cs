using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    [field: SerializeField] public WeaponSO weaponBaseStats { get; private set; }

    private float _chargePercent = 0f;
    private Coroutine _chargingRoutine;
    private bool _canFire = true;
    private bool _holdingCharge = false;

    [Header("References")]
    [SerializeField] private InputManager inputManager;

    [Header("Events")]
    public GameEvent weaponFiredEvent;

    protected Transform playerCameraTransform;

    private void OnEnable()
    {
        inputManager.attackReleaseEvent += OnAttackRelease;
        inputManager.attackChargeEvent += OnAttackCharge;
        _canFire = true;
    }

    private void OnDisable()
    {
        inputManager.attackReleaseEvent -= OnAttackRelease;
        inputManager.attackChargeEvent -= OnAttackCharge;
    }

    private void Awake()
    {
        playerCameraTransform = Camera.main.transform;    
    }

    private void OnAttackRelease()
    {
        _holdingCharge = false;
        if (weaponBaseStats.MaxChargeUpTime > 0)
        {
            if (_chargingRoutine != null)
            {
                StopCoroutine(_chargingRoutine);
                _chargePercent = 0;
            }
            if (weaponBaseStats.MinChargePercent <= _chargePercent && 
                _canFire)
            {
                PerformAttack();
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

    private IEnumerator WaitBetweenAttacks()
    {
        _canFire = false;
        yield return weaponBaseStats.CoolDownWait;
        _canFire = true;
        if(_holdingCharge && (weaponBaseStats.IsFullyAuto || weaponBaseStats.MaxChargeUpTime > 0))
        {
            OnAttackCharge();
        }
    }

    private void PerformAttack()
    {
        weaponFiredEvent.Raise(this, null);
        Fire();
        StartCoroutine(WaitBetweenAttacks());
        _chargePercent = 0f;
    }
}
