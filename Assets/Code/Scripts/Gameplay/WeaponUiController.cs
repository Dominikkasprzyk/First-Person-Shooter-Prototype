using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUiController : MonoBehaviour
{
    [SerializeField] private Slider _weaponCooldownSlider;
    [SerializeField] private Slider _weaponChargeSlider;
    [SerializeField] private float _sliderUpdateTimeStep = 0.1f;
    [Tooltip("Show cooldown slider for weapons with cooldown above this one.")]
    [Min(0)][SerializeField] private float _showCooldownThreshold;

    private WeaponSO _currentWeaponStats;
    private Coroutine _chargeRoutine, _cooldownRoutine;

    public void AdjustUiToWeapon(Component sender, object data)
    {
        StopAllCoroutines();
        _currentWeaponStats = (WeaponSO)data;
        _weaponCooldownSlider.value = 1;
        _weaponChargeSlider.value = 0;
        if (_currentWeaponStats.PreparationTime > 0)
            StartCoroutine(SliderRoutine(_weaponCooldownSlider, _currentWeaponStats.PreparationTime));
    }

    public void ActivateCooldownSlider(Component sender, object data)
    {
        if (_currentWeaponStats)
        {
            _weaponChargeSlider.gameObject.SetActive(false);
            if(_cooldownRoutine != null)
            {
                StopCoroutine(_cooldownRoutine);
            }
            if (_currentWeaponStats.TimeBetweenAttacks > _showCooldownThreshold)
            {
                _cooldownRoutine = StartCoroutine(SliderRoutine(_weaponCooldownSlider, _currentWeaponStats.TimeBetweenAttacks));
            }
        }
    }

    public void ActivateChargeSlider(Component sender, object data)
    {
        if((bool)data)
        {
            if (_currentWeaponStats)
            {
                if (_currentWeaponStats.MaxChargeUpTime > 0)
                    _chargeRoutine = StartCoroutine(SliderRoutine(_weaponChargeSlider, _currentWeaponStats.MaxChargeUpTime));
            }
        } else
        {
            _weaponChargeSlider.value = 0;
            if (_chargeRoutine != null)
            {
                StopCoroutine(_chargeRoutine);
                _weaponChargeSlider.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator SliderRoutine(Slider slider, float timeToWait)
    {
        slider.gameObject.SetActive(true);
        slider.value = 0;
        float currentTime = 0f;
        while (currentTime < timeToWait)
        {
            yield return new WaitForSeconds(_sliderUpdateTimeStep);
            currentTime += _sliderUpdateTimeStep;
            slider.value = currentTime / timeToWait;
            if(slider.value > 1)
            {
                slider.value = 1;
            }
        }
        slider.gameObject.SetActive(false);
    }
}
