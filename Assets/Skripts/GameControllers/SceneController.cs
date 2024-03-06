using Cinemachine;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController  : MonoBehaviour
{
    public bool IsNewGame;
    public bool CameraFollowingPlayer;
    public int sceneIndex = 0;

    SaveLoadPlayerData saveData;
    Player player;

    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] Fade fade;

    [Inject]
    private void Construct(SaveLoadPlayerData saveData, Player player)
    {
        this.saveData = saveData;
        this.player = player;
    }

    private void OnEnable()
    {
        player.Health.Died += RestartLevel;
    }

    private void Start()
    {
        if(!IsNewGame)
        {
            saveData.LoadPlayerData();
        }

        if(CameraFollowingPlayer)
        {
            virtualCamera.Follow = player.transform;
        }
        fade.Out();
    }

    public void LoadNextLevevl()
    {
        saveData.SavePlayerData();
        StartCoroutine(LoadSceneWithDilay(++sceneIndex));
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadSceneWithDilay(sceneIndex));
    }

    private void OnDisable()
    {
        player.Health.Died -= RestartLevel;
    }

    private IEnumerator LoadSceneWithDilay(int scebeIndex)
    {
        fade.In();
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(scebeIndex);
    }
}
