

public class FlyingEyeHealth : Health
{
    private void Start()
    {
        MaxHealth = 30;
        CurrentHealth = MaxHealth;
    }

    public override bool IsCanBeRestored(int healthtoRestore)
    {
        return false;
    }
}
