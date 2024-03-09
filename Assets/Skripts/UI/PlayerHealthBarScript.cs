using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarScript : MonoBehaviour
{
    [SerializeField] private Image healthBarFilling;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Player player;
    private float percentage;

    void Start()
    {
        player.Health.Changed += OnPlayerHealthChanged;
        OnPlayerHealthChanged(player.Health.MaxHealth, player.Health.CurrentHealth);
    }

    private void OnPlayerHealthChanged(int maxHealth, int newHealth)
    {
        percentage = CalculateSliderPercentage(maxHealth, newHealth);
        healthSlider.value = percentage;
        healthBarFilling.color = gradient.Evaluate(percentage);
    }

    private float CalculateSliderPercentage( float maxHealth, float currentHealth)
    {
        return currentHealth / maxHealth;
    }
}
