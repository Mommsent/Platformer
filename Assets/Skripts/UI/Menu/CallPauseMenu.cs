using UnityEngine;
using UnityEngine.EventSystems;

public class CallPauseMenu : MonoBehaviour
{
    private bool gameIsPaused;
    [SerializeField] private Player player;
    [Header("Menu")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settings;

    [SerializeField] private GameObject pauseMenuFirst;
    [SerializeField] private GameObject settingsMenuFirst; 

    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;

        if (gameIsPaused)
        {
            OpenMainMenu();
            Time.timeScale = 0;
            player.CanMove = false;
        }
        else
        {
            CloseMenus();
            Time.timeScale = 1;
            player.CanMove = true;
        }
    }

    public void OpenSettingsMenu()
    {
        settings.SetActive(true);
        pauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(settingsMenuFirst);
    }

    public void OpenMainMenu()
    {
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(pauseMenuFirst);
    }

    public void CloseMenus()
    {
        pauseMenu.SetActive(false);
        settings.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
