using UnityEngine;
using Zenject;

public class ProjectileLauncher : MonoBehaviour
{
    public Projectile projectilePrefab;
    public GameObject projectileLauncher;

    public IAmmo Ammo { get; set; }
    [Inject]
    public void Construct(IAmmo ammo)
    {
        this.Ammo = ammo;
    }

    public void Fire()
    {
        Projectile projectile = Instantiate(projectilePrefab, projectileLauncher.transform.position, projectilePrefab.transform.rotation);
        Vector3 origScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(origScale.x * transform.localScale.x > 0 ? origScale.x * 1 : origScale.x  * (-1), origScale.y, origScale.z);
        Ammo.AmmoAmount--;
    }
}
