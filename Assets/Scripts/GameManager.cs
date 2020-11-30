using System;
using System.Collections;
using System.Collections.Generic;
using Castle;
using Manager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Ctx;

    public Minions.Archer archerPrefab;
    public GameObject playerCastleObject;
    public GameObject enemyCastleObject;
    
    public MinionsManager MinionsManger { get; private set; }

    internal void Start()
    {
        Ctx = this;
        // playerCastleObject = GameObject.Find("PlayerCastle");
        // enemyCastleObject = GameObject.Find("EnemyCastle");
        MinionsManger = gameObject.AddComponent<MinionsManager>();
        InitializeCastleStatus();
        
    }

    private void InitializeCastleStatus()
    {
        EnemyCastle.Health = 200;
        PlayerCastle.Health = 200;
        PlayerCastle.MoneyRate = 20;
    }
    
}