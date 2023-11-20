using System.Collections;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float singleShotTime = 0.2f;

    private Laser laser;

    private void OnEnable()
    {
        foreach (Transform child in firePoint)
        {
            Destroy(child.gameObject);
        }
    }

    public void EnableLaser()
    {
        foreach (Transform child in firePoint)
        {
            Destroy(child.gameObject);
        }
        GameObject laserGameObject = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        laserGameObject.transform.parent = firePoint;
        laser = laserGameObject.GetComponent<Laser>();
        StartCoroutine(DisableLaserRoutine());
    }

    private IEnumerator DisableLaserRoutine()
    {
        yield return new WaitForSeconds(singleShotTime);
        if (laser != null)
        {
            laser.DisablePrepare();
        }
    }
}
