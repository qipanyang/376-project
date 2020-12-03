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
        public WomanArcher WomanArcherPrefab;
        public WomanAttacker WomanAttackerPrefab;
        public WomanWarrior WomanWarrior;
        public ElfArcher ElfArcher;
        public ElfAttacker ElfAttacker;
        public KnightPikeman KnightPikeman;
        public KnightWarrior KnightWarrior;

        public List<Minion> EnemyMinions = new List<Minion>();
        public List<Minion> PlayerMinions = new List<Minion>();

        internal void Start()
        {
            InvokeRepeating(nameof(SpawnEnemyMinions), 0, 3f);
        }

        public void SpawnEnemyMinions()
        {
            if (EnemyMinions.Count <= 10)
            {
                Minion minion = Instantiate(WomanArcherPrefab, Data.GetEnemyCastlePosition(),
                    Data.GetEnemyFacing());
                minion.Initialize(Data.GetWomanArcherMinionData(), MinionSide.Enemy);
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

        private void GenerateMinion(MinionData minionData, Minion prefab)
        {
            var goldManager = GameManager.Ctx.GoldManager;
            if (goldManager.DecreaseGold(minionData.Price) == false)
            {
                return;
            }

            Minion minion = Instantiate(prefab, Data.GetPlayerCastlePosition(),
                Data.GetPlayerFacing());
            minion.Initialize(minionData, MinionSide.Player);
            PlayerMinions.Add(minion);
        }

        public void GenerateWomanArcher()
        {
            GenerateMinion(Data.GetWomanArcherMinionData(), WomanArcherPrefab);
        }

        public void GenerateWomanAttacker()
        {
            GenerateMinion(Data.GetWomanAttackerMinionData(), WomanAttackerPrefab);
        }

        public void GenerateWomanWarrior()
        {
            GenerateMinion(Data.GetWomanWarriorMinionData(), WomanWarrior);
        }

        public void GenerateElfArcher()
        {
            GenerateMinion(Data.GetElfArcherMinionData(), ElfArcher);
        }

        public void GenerateElfAttacker()
        {
            GenerateMinion(Data.GetElfAttackerMinionData(), ElfAttacker);
        }

        public void GenerateKnightPikeman()
        {
            GenerateMinion(Data.GetKnightPikemanMinionData(), KnightPikeman);
        }

        public void GenerateKnightWarrior()
        {
            GenerateMinion(Data.GetKnightWarriorMinionData(), KnightWarrior);
        }

        public MinionListSaveData OnSave()
        {
            MinionListSaveData minionListSaveData = new MinionListSaveData();
            var minionObjects = FindObjectsOfType(typeof(Minion));
            foreach (Minion oneMinionObject in minionObjects)
            {
                MinionSaveData minion = new MinionSaveData();
                minion.minionSide = oneMinionObject.minionSide;
                minion.minionData = oneMinionObject.minionData;
                minion.Pos = oneMinionObject.GetComponent<Rigidbody2D>().position;
                //minion.rd = oneMinionObject.rd;
                minion.lastAttackTime = oneMinionObject.lastAttackTime;
                minionListSaveData.Minions.Add(minion);

            }
            return minionListSaveData;
        }
    }

    public class MinionListSaveData
    {
        public List<MinionSaveData> Minions = new List<MinionSaveData>();
    }

    public class MinionSaveData
    {
        public MinionSide minionSide;
        public MinionData minionData;
        public Vector2 Pos;
        //public Rigidbody2D rb;
        //public Renderer rd;
        public float lastAttackTime;
    }

}