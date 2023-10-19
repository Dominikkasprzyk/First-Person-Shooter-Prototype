using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public event UnityAction<float> healthPercentChangedEvent;

    [SerializeField] private UnityEvent destroyedEvent;
    public Fabric madeOf;
    [SerializeField] private float maxHealthPoints = 10f;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealthPoints;
    }

    public void TakeHit(float _damage, Fabric _type)
    {
        if (FabricData.damages.ContainsKey(madeOf))
        {
            if (FabricData.damages[madeOf].Contains(_type))
            {
                TakeDamage(_damage);
            }
        }
    }

    private void TakeDamage(float damage)
    { 
        currentHealth -= damage;
        healthPercentChangedEvent.Invoke(currentHealth/maxHealthPoints);
        if (currentHealth <= 0)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        destroyedEvent.Invoke();
        Destroy(gameObject);
    }
}
