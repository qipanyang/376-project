
using System;
using UnityEngine;
using UnityEngine.UI;

public class TextManager
{
    private readonly Text _playerCastleHealth;
    private readonly Text _enemyCastleHealth;

    public TextManager()
    {
        _playerCastleHealth = GameObject.Find("PlayerCastleHealth").GetComponent<Text>();
        _enemyCastleHealth = GameObject.Find("EnemyCastleHealth").GetComponent<Text>();
    }
    
    public void UpdatePlayerCastleHealth(int health) {
        _playerCastleHealth.text = $"Player Castle Health: {health}";
    }
    
    public void UpdateEnemyCastleHealth(int health) {
        _enemyCastleHealth.text = $"Enemy Castle Health: {health}";
    }

}
