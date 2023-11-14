using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [Header("Weapon parameters")]
    [SerializeField] private float damage = 1f;
    [SerializeField] private Fabric damageType;
    [Min(0f)]
    [SerializeField] private float range = 50f;
    

    [Header("References")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameEvent weaponChangedEvent;

    public UnityEvent weaponFiredEvent;

    private Transform playerCameraTransform;

    private void OnEnable()
    {
        inputManager.attackEvent += PerformAttack;
        if(weaponChangedEvent != null)
        {
            weaponChangedEvent.Raise(this, damageType);
        }
    }

    private void OnDisable()
    {
        inputManager.attackEvent -= PerformAttack;
    }

    private void Awake()
    {
        playerCameraTransform = Camera.main.transform;    
    }

    public void PerformAttack()
    {
        weaponFiredEvent.Invoke();
        RaycastHit hit;
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, range))
        {
            if(hit.collider.GetComponent<Damageable>() != null)
            {
                hit.collider.GetComponent<Damageable>().TakeHit(damage, damageType);
            }
        }
    }
}
