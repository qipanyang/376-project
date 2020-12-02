using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Minions;
using UnityEngine;

namespace Manager
{
    public class MinionsManager : MonoBehaviour
    {
        public Archer archerPrefab;
        
        public List<Minion> EnemyMinions = new List<Minion>();
        public List<Minion> PlayerMinions = new List<Minion>();

        internal void Start()
        {
            InvokeRepeating(nameof(SpawnEnemyMinions), 0, 1f);
        }

        public void SpawnEnemyMinions()
        {
            if (EnemyMinions.Count <= 10)
            {
                Minion minion = Instantiate(archerPrefab, Data.GetEnemyCastlePosition(),
                    Data.GetEnemyFacing());
                minion.Initialize(Data.GetArcherMinionData(), MinionSide.Enemy);
                EnemyMinions.Add(minion);   
            }
            
        }

        private void Update()
        {
            RemoveDeadMinions(EnemyMinions);
            RemoveDeadMinions(PlayerMinions);
        }

        private void RemoveDeadMinions(List<Minion> minions)
        {
            foreach (var minion in minions)
            {
                if (minion.IsDead())
                {
                    minion.DestroyGameObject();
                }
            }
            minions.RemoveAll(minion => minion.IsDead());
        }

        public void GenerateArcher()
        {
            var archerMinionData = Data.GetArcherMinionData();
            var goldManager = GameManager.Ctx.GoldManager;
            if (goldManager.DecreaseGold(archerMinionData.Price) == false)
            {
                return;
            }
            
            Minion minion = Instantiate(archerPrefab, Data.GetPlayerCastlePosition(),
                Data.GetPlayerFacing());
            minion.Initialize(archerMinionData, MinionSide.Player);
            PlayerMinions.Add(minion);
        }
    }
}