using Zenject;
using UnityEngine;

public class GameplaySceneMonoInstaller : MonoInstaller
{
    [SerializeField] private GameObject HealthTextSpawnerPrefab;
    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private Transform spawnPos;
    public override void InstallBindings()
    {
        Container.Bind<IAmmo>().To<Arrows>().AsSingle();
        Container.Bind<SaveLoadPlayerData>().AsSingle();
        Container.Bind<IDamageable>().To<PlayerHealth>().AsSingle();

        Container.Bind<HealthTextSpawner>().FromComponentInNewPrefab(HealthTextSpawnerPrefab).AsSingle();

        InstantiatePlayer();
    }

    private void InstantiatePlayer()
    {
        Player player = Container.InstantiatePrefabForComponent<Player>(PlayerPrefab, spawnPos.transform.position, Quaternion.identity, null);
        Container.BindInterfacesAndSelfTo<Player>().FromInstance(player).AsSingle();
    }
}