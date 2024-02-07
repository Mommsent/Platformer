using UnityEngine;

public class PlayerHealth : Health, IHealable
{
    public virtual bool IsCanBeRestored(int healthToRestore)
    {
        if (IsAlive && MaxHealth > CurrentHealth)
        {
            return true;
        }
        return false;
    }

    public virtual void Restore(int healthRestore)
    {
        int maxHeal = Mathf.Max(MaxHealth - CurrentHealth, 0);
        int actualhea = Mathf.Min(maxHeal, healthRestore);

        healtTextSpawner.CharacterHealed(this, actualhea);

        CurrentHealth += actualhea;

        Changed?.Invoke(MaxHealth, CurrentHealth);
    }
}
