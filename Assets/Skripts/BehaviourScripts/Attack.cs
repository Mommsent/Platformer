using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Vector2 knockback = Vector2.zero;
    [SerializeField] private int attackDamage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageeable = collision.GetComponent<Damageable>();
        if (damageeable != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            damageeable.Hit(attackDamage, deliveredKnockback);
        }
    }
}
