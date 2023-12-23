using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GunEffectsController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _chargeParticleSystem;
    [SerializeField] private GameObject _fireEffect;
    [SerializeField] private Transform _firePoint;

    private ObjectPool<GameObject> _firePool;

    private void Awake()
    {
        _chargeParticleSystem = Instantiate(_chargeParticleSystem, _firePoint.position, _firePoint.rotation, _firePoint);
    }

    public void ActivateChargeEffect(Component sender, object data)
    {
        if (sender is Weapon)
        {
            if ((bool)data)
            {
                _chargeParticleSystem.Play();
            }
            else
            {
                _chargeParticleSystem.Stop();
            }
        }
    }

    private void Start()
    {
        _firePool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(_fireEffect);
        }, spawned =>
        {
            spawned.gameObject.SetActive(true);
        }, spawned =>
        {
            spawned.gameObject.SetActive(false);
        }, spawned =>
        {
            Destroy(spawned.gameObject);
        }, false, 2, 2);
    }

    public void ActivateFireEffect(Component sender, object data)
    {
        GameObject spawned = _firePool.Get();
        spawned.transform.SetPositionAndRotation(_firePoint.position, _firePoint.rotation);
        spawned.transform.SetParent(_firePoint);
    }
}
