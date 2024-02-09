using System.Collections;
using UnityEngine;

interface IDamageable 
{
    public int CurrentHealth { get; set; }

    void Reduce(int damage, Vector2 knockback);
    bool IsCanBeReduced();
    IEnumerator invincibleTime(float time);
}
