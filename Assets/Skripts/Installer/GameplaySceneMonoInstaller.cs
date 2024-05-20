using Zenject;
using UnityEngine;

public class GameplaySceneMonoInstaller : MonoInstaller
{
    [SerializeField] private GameObject HealthTextSpawnerPrefab;
    [SerializeField] private PlayerHealth health;

    public override void InstallBindings()
    {
        Container.
            Bind<IAmmo>().To<ArrowsAmmo>().
            AsSingle();

        Container.
            Bind<SaveLoadPlayerData>().
            AsSingle();

        Container.
            Bind<HealthTextSpawner>().FromComponentInNewPrefab(HealthTextSpawnerPrefab).
            AsSingle();

        Container.
            Bind<PlayerHealth>().FromInstance(health).
            AsSingle();
    }
}