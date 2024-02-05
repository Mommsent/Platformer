using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public TMP_Text damageTextPrefab;
    public TMP_Text healthTextPrefab;
    public Canvas gameCanvas;
    [SerializeField] Health health;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
        health.gotDamage += CharacterTookDamage;
        health.Restored += CharacterHealed;
    }

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

    public void OnExitGame(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
                Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            #endif

            #if (UNITY_EDITOR)
                    UnityEditor.EditorApplication.isPlaying = false;
            #elif (UNITY_STANDALONE)
                    Application.Quit();
            #elif (UNITY_WEBGL)
                    SceneManager.LoadScene("QuitScene");
            #endif
        }
    }
}
