using Cinemachine;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController  : MonoBehaviour
{
    [SerializeField] private bool IsNewGame;
    [SerializeField] private bool CameraFollowingPlayer;
    [SerializeField] private int sceneIndex = 0;
    private float standartDilay = 3f;

    SaveLoadPlayerData saveData;
    
    private PlayerHealth health;
    [SerializeField] private Transform playerPos;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Fade fade;
    [SerializeField] private AudioSource audioSource;

    [Inject]
    private void Constract(PlayerHealth player)
    {
        this.health = player;
    }

    [Inject]
    private void Construct(SaveLoadPlayerData saveData)
    {
        this.saveData = saveData;
    }

    private void Start()
    {
        if (!IsNewGame)
        {
            saveData.LoadPlayerData();
        }

        if(CameraFollowingPlayer)
        {
            virtualCamera.Follow = playerPos.transform;
        }

        fade.Out();
        health.Died += RestartLevel;
    }

    public void LoadNextLevevl(float dilay)
    {
        saveData.SavePlayerData();
        fade.In(audioSource);
        StartCoroutine(LoadSceneWithDilay(++sceneIndex, dilay));
    }

    public void RestartLevel()
    {
        fade.In(audioSource);
        StartCoroutine(LoadSceneWithDilay(sceneIndex, standartDilay));
    }

    private IEnumerator LoadSceneWithDilay(int scebeIndex,float dilay)
    {
        yield return new WaitForSeconds(dilay);
        SceneManager.LoadScene(scebeIndex);
    }
    
    private void OnDisable()
    {
        health.Died -= RestartLevel;
    }
}
