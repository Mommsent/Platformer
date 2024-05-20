using UnityEngine;

public class ProjectileSpawner: MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform spawnPos;

    public void Spawn()
    {
        Projectile projectile = Instantiate(projectilePrefab, spawnPos.transform.position, spawnPos.transform.rotation);
        Vector3 origScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(origScale.x * transform.localScale.x > 0 ? origScale.x * 1 : origScale.x * (-1), origScale.y, origScale.z);
    }
}
