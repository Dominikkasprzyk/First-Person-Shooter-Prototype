using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUiController : MonoBehaviour
{
    [SerializeField] private Slider _weaponCooldownSlider;
    [SerializeField] private Slider _weaponChargeSlider;
    [SerializeField] private float _sliderUpdateTimeStep = 0.1f;

    private WeaponSO _currentWeaponStats;
    private Coroutine _chargeRoutine;

    public void AdjustUiToWeapon(Component sender, object data)
    {
        StopAllCoroutines();
        _currentWeaponStats = (WeaponSO)data;
        _weaponCooldownSlider.value = 1;
        _weaponChargeSlider.value = 0;
        if (_currentWeaponStats.TimeBetweenAttacks > 0)
            StartCoroutine(SliderRoutine(_weaponCooldownSlider, _currentWeaponStats.PreparationTime));
        
        if (_currentWeaponStats.MaxChargeUpTime > 0)
        {
            _weaponChargeSlider.gameObject.SetActive(true);
        } else
        {
            _weaponChargeSlider.gameObject.SetActive(false);
        }

        if(_currentWeaponStats.TimeBetweenAttacks > 0)
        {
            _weaponCooldownSlider.gameObject.SetActive(true);
        } else
        {
            _weaponCooldownSlider.gameObject.SetActive(false);
        }
    }

    public void ActivateCooldownSlider(Component sender, object data)
    {
        if (_currentWeaponStats)
        {
            _weaponChargeSlider.value = 0;
            if (_chargeRoutine != null)
                StopCoroutine(_chargeRoutine);
            if (_currentWeaponStats.TimeBetweenAttacks > 0)
            {
                StartCoroutine(SliderRoutine(_weaponCooldownSlider, _currentWeaponStats.TimeBetweenAttacks));
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
            if(_chargeRoutine != null)
                StopCoroutine(_chargeRoutine);
        }
    }

    private IEnumerator SliderRoutine(Slider slider, float timeToWait)
    {
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
    }
}
