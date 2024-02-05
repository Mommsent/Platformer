using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider healthSlider;
    [SerializeField] private Health health;
    private void OnEnable()
    {
        health.Changed += OnPlayerHealthChanged;
    }

    void Start()
    {
        OnPlayerHealthChanged(health.CurrentHealth, health.MaxHealth);
    }

    private void OnPlayerHealthChanged(int maxHealth, int newHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }
}
