using UnityEngine;

public class SaveLoadPlayerData
{
    public void SavePlayerData(Player player)
    {
        PlayerPrefs.SetInt("Health", player.Health.CurrentHealth);
        PlayerPrefs.SetInt("Ammo", player.Ammo.AmmoAmount);
    }

    public void LoadPlayerData(Player player) 
    {
        player.Health.CurrentHealth = PlayerPrefs.GetInt("Health");
        player.Ammo.AmmoAmount = PlayerPrefs.GetInt("Ammo");
    }
}
