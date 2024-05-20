using UnityEngine;
using UnityEngine.UI;

public class HeatlhBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image healthBarFilling;
    [SerializeField] private Gradient gradient;
    
    [SerializeField] private Health health;

    private float percentage;

    private void OnEnable()
    {
        health.Changed += OnPlayerHealthChanged;
    }

    void Start()
    {
        OnPlayerHealthChanged(health.MaxHealth, health.CurrentHealth);
    }

    private void OnPlayerHealthChanged(int maxHealth, int newHealth)
    {
        percentage = CalculateSliderPercentage(maxHealth, newHealth);
        healthSlider.value = percentage;
        healthBarFilling.color = gradient.Evaluate(percentage);
    }

    private float CalculateSliderPercentage(float maxHealth, float currentHealth)
    {
        return currentHealth / maxHealth;
    }

    private void OnDisable()
    {
        health.Changed -= OnPlayerHealthChanged;
    }
}
