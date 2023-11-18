using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private int _defaultPoolCapacity = 3;
    [SerializeField] private int _maxPoolCapacity = 10;

    private ObjectPool<GameObject> _pool;

    private void Start()
    {
        _pool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(_objectToSpawn);
        }, spawned =>
        {
            spawned.gameObject.SetActive(true);
        }, spawned =>
        {
            spawned.gameObject.SetActive(false);
        }, spawned =>
        {
            Destroy(spawned.gameObject);
        }, false, _defaultPoolCapacity, _maxPoolCapacity);
    }

    public void Spawn(Component sender, object data)
    {
        GameObject spawned = _pool.Get();
        Pose spawnedPose = (Pose)data;
        spawned.transform.position = spawnedPose.position;
    }
}
