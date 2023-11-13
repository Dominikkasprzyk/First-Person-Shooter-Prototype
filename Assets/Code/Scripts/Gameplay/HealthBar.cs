using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text fabricInfo;
    [SerializeField] private Damageable target;
    [SerializeField] private Vector3 offset;

    private void OnEnable()
    {
        target.healthPercentChangedEvent += UpdateHealthBar;
        fabricInfo.text = target.madeOf.ToString();
    }

    private void OnDisable()
    {
        target.healthPercentChangedEvent -= UpdateHealthBar;
    }

    private void UpdateHealthBar(float _percent)
    {
        healthSlider.value = _percent;
    }

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.position = target.transform.position + offset;
    }
}
