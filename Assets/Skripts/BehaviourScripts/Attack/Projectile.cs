using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public Vector2 moveSpeed = new Vector2(20f, 0);
    public Vector2 knockback = new Vector2 (0, 0);
    Rigidbody2D rb;
    IDamageable damageable;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        bool checkIfHas = collision.TryGetComponent(out damageable);
        if (checkIfHas)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            bool gotHit = damageable.IsCanBeReduced();
            if (gotHit) 
            {
                damageable.Reduce(damage, deliveredKnockback);
                Destroy(gameObject);
            }
        }
    }
}
