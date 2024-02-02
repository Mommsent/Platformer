using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider healthSlider;
    Damageable damageable;
    private void OnEnable()
    {
        damageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player == null )
        {
            Debug.Log("No player found");
        }
        damageable = player.GetComponent<Damageable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnPlayerHealthChanged(damageable.Health, damageable.MaxHealth);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
    }
}
