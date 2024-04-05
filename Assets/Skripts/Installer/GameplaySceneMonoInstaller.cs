using Zenject;
using UnityEngine;

public class GameplaySceneMonoInstaller : MonoInstaller
{
    [SerializeField] private GameObject HealthTextSpawnerPrefab;
    public override void InstallBindings()
    {
        Container.Bind<IAmmo>().To<Arrows>().AsSingle();
        Container.Bind<SaveLoadPlayerData>().AsSingle();
        Container.Bind<IDamageable>().To<PlayerHealth>().AsSingle();

        Container.Bind<HealthTextSpawner>().FromComponentInNewPrefab(HealthTextSpawnerPrefab).AsSingle();
    }
}