using UnityEngine;
using Zenject;

public class SaveLoadPlayerData
{
    private PlayerHealth health;
    private IAmmo ammo;

    [Inject]
    private void Constract(PlayerHealth health)
    {
        this.health = health;
    }

    [Inject]
    private void Constract(IAmmo ammo)
    {
        this.ammo = ammo;
    }

    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("Health", health.CurrentHealth);
        PlayerPrefs.SetInt("Ammo", ammo.AmmoAmount);
    }

    public void LoadPlayerData()
    {
        health.CurrentHealth = PlayerPrefs.GetInt("Health");
        ammo.AmmoAmount = PlayerPrefs.GetInt("Ammo");
    }
}
