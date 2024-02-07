using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Projectile projectilePrefab;
    public GameObject projectileLauncher;

    public void Fire()
    {
        Projectile projectile = Instantiate(projectilePrefab, projectileLauncher.transform.position, projectilePrefab.transform.rotation);
        Vector3 origScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(origScale.x * transform.localScale.x > 0 ? 1 : -1, origScale.y, origScale.z);
    }
}
