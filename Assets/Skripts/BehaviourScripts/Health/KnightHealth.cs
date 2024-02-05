

public class KnightHealth : Health
{
    private void Start()
    {
        MaxHealth = 50;
        CurrentHealth = MaxHealth;
    }

    public override bool IsCanBeRestored(int healthtoRestore)
    {
        return false;
    }
}
