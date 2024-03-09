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
    
    [SerializeField] private Player player;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Fade fade;
    [SerializeField] private AudioSource audioSource;
    
    [Inject]
    private void Construct(SaveLoadPlayerData saveData)
    {
        this.saveData = saveData;
    }

    private void Start()
    {
        player.Health.Died += RestartLevel;

        if (!IsNewGame)
        {
            saveData.LoadPlayerData(player);
        }

        if(CameraFollowingPlayer)
        {
            virtualCamera.Follow = player.transform;
        }

        fade.Out();
    }

    public void LoadNextLevevl()
    {
        saveData.SavePlayerData(player);
        fade.In(audioSource);
        StartCoroutine(LoadSceneWithDilay(++sceneIndex));
    }

    public void RestartLevel()
    {
        fade.In(audioSource);
        StartCoroutine(LoadSceneWithDilay(sceneIndex));
    }

    private IEnumerator LoadSceneWithDilay(int scebeIndex)
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(scebeIndex);
    }
    
    private void OnDisable()
    {
        player.Health.Died -= RestartLevel;
    }
}
