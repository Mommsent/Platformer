using System;
public interface IAmmo
{
    public int AmmoAmount {  get; set; }
    public delegate void Changed(int damage);
    public event Changed wasChanged;

}
