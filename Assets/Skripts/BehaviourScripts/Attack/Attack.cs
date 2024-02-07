using UnityEngine;

public class Attack : MonoBehaviour
{
    public Vector2 knockback = Vector2.zero;
    [SerializeField] private int attackDamage = 10;
    IDamageable damageable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool checkIfHas = collision.TryGetComponent(out damageable);
        if (checkIfHas)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            bool gotHit = damageable.IsCanBeReduced();
            if (gotHit)
            {
                damageable.Reduce(attackDamage, deliveredKnockback);
            }
        }
    }
}
