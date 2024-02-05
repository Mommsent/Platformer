using System.Collections;
using UnityEngine;

interface IDamageable 
{
    public bool LockVelocity {  get; set; }
    public int CurrentHealth { get; set; }

    void Reduce(int damage, Vector2 knockback);
    bool IsCanBeReduced();
    IEnumerator invincibleTime(float time);
}
