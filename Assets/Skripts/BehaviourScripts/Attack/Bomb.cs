using UnityEngine;
public class Bomb : Projectile
{
    [SerializeField] internal Animator animator;
    public override void DestroyProjectile()
    {
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Collided");
    }
}
