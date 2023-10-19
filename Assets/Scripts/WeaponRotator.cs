using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
	[SerializeField] private float rotationSpeed;
    [SerializeField] private LayerMask ignoredLayers;

    private Quaternion lookRotation;
	private Vector3 direction;
	private Transform playerCameraTransform;

    private void Awake()
    {
        playerCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, 100f, ~ignoredLayers))
        {
            direction = (hit.point - transform.position).normalized;
            lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
		}
    }
}
