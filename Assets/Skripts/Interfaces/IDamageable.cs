using System.Collections;
using UnityEngine;

public interface IDamageable 
{
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }

    void Reduce(int damage, Vector2 knockback);
    bool IsCanBeReduced();
    IEnumerator invincibleTime(float time);
}
