using TMPro;
using UnityEngine;

public class HealthTextSpawner : MonoBehaviour
{
    [SerializeField] private TMP_Text damageTextPrefab;
    [SerializeField] private TMP_Text healthTextPrefab;
    [SerializeField] private Canvas gameCanvas;

    public void CharacterTookDamage(Health character, int damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();
        tmpText.text = damageReceived.ToString();
    }
    
    public void CharacterHealed(Health character, int healthRestored)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();
        tmpText.text = healthRestored.ToString();
    }
}
