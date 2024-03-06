using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerHealthBarScript : MonoBehaviour
{
    public Slider healthSlider;
    private Player player;

    [Inject]
    private void Construct(Player player)
    {
        this.player = player;
    }

    private void OnEnable()
    {
        player.Health.Changed += OnPlayerHealthChanged;
    }

    void Start()
    {
        OnPlayerHealthChanged(player.Health.MaxHealth, player.Health.CurrentHealth);
    }

    private void OnPlayerHealthChanged(int maxHealth, int newHealth)
    {
        healthSlider.value = CalculateSliderPercentage(maxHealth, newHealth);
    }

    private float CalculateSliderPercentage( float maxHealth, float currentHealth)
    {
        return currentHealth / maxHealth;
    }
}
