public class ArrowsAmmo : IAmmo
{
    public event IAmmo.Changed wasChanged;

    private int _ammoCount = 5;
    public int AmmoAmount 
    { 
        get
        {
            return _ammoCount;
        }
        set
        {
            _ammoCount = value;
            wasChanged?.Invoke(_ammoCount);
        }
    }
}
