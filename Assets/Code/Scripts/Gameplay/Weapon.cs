using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    [field: SerializeField] public WeaponSO weaponBaseStats { get; private set; }
    [SerializeField] private bool _isContinous;

    private float _chargePercent = 0f;
    private Coroutine _chargingRoutine;
    private bool _canFire = true;
    private bool _waitingForCharge = false;

    [Header("References")]
    [SerializeField] private InputManager inputManager;

    [Header("Events")]
    public GameEvent weaponFiredEvent;

    protected Transform playerCameraTransform;

    private void OnEnable()
    {
        inputManager.attackReleaseEvent += OnAttackRelease;
        inputManager.attackChargeEvent += OnAttackCharge;
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
        _waitingForCharge = false;
        if (weaponBaseStats.MaxChargeUpTime > 0)
        {
            if (_chargingRoutine != null)
                StopCoroutine(_chargingRoutine);
            if (weaponBaseStats.MinChargePercent <= _chargePercent && _canFire)
            {
                weaponFiredEvent.Raise(this, null);
                Fire();
                StartCoroutine(WaitBetweenAttacks());
            }
            _chargePercent = 0;
        }
    }

    abstract protected void Fire();

    private void OnAttackCharge()
    {
        if (weaponBaseStats.MaxChargeUpTime <= 0 && _canFire)
        {
            weaponFiredEvent.Raise(this, null);
            Fire();
            StartCoroutine(WaitBetweenAttacks());
        }
        else
        {
            if (_canFire)
            {
                _chargingRoutine = StartCoroutine(ChargeWeaponUp());
            } else
            {
                _waitingForCharge = true;
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
    }

    private IEnumerator WaitBetweenAttacks()
    {
        _canFire = false;
        yield return weaponBaseStats.CoolDownWait;
        _canFire = true;
        if(_waitingForCharge)
        {
            OnAttackCharge();
        }
    }
}
