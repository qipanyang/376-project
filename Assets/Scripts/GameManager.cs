using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Minions.Archer archerPrefab;
    public GameObject playerCastleObject;

    public void Start()
    {
        playerCastleObject = GameObject.Find("PlayerCastle");
    }

    public void GenerateArcher()
    {
        Vector3 playerCastlePosition = playerCastleObject.transform.position;
        Instantiate(archerPrefab, new Vector3(playerCastlePosition.x, playerCastlePosition.y - 0.5f, 0),
            Quaternion.Euler(0, 180, 0));
        archerPrefab.Initialize(100, 10, -2);
    }
}