using UnityEngine;
using Zenject;

public class LoadPlayerDataOnLevelStart : MonoBehaviour
{
    private SaveLoadPlayerData data;

    [Inject]
    private void Construct(SaveLoadPlayerData data)
    {
        this.data = data;
    }

    private void Start()
    {
        data.LoadPlayerData();
    }
}
