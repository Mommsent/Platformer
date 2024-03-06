using UnityEngine;

public class CallPauseMenu : MonoBehaviour
{
    [Header("Menu")]
    private bool gameIsPaused;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject Settings;

    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;

        if (gameIsPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            Settings.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
