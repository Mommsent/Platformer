using Zenject;
using System;
public class Arrows : IAmmo
{
    private int ammoCount = 3;

    public event IAmmo.Changed wasChanged;

    public int AmmoAmount 
    { 
        get
        {
            return ammoCount;
        }
        set
        {
            ammoCount = value;
            wasChanged?.Invoke(ammoCount);
        }
    }
}
