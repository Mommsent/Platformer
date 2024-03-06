using UnityEngine;
using Zenject;

public class SaveLoadPlayerData
{
    private Player player;

    [Inject]
    private void Construct(Player player)
    {
        this.player = player;
    }

    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("Health", player.Health.CurrentHealth);
        PlayerPrefs.SetInt("Ammo", player.Ammo.AmmoAmount);
    }

    public void LoadPlayerData() 
    {
        player.Health.CurrentHealth = PlayerPrefs.GetInt("Health");
        player.Ammo.AmmoAmount = PlayerPrefs.GetInt("Ammo");
    }
}
