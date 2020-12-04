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

                minion.Initialize(Data.GetWomanArcherMinionData(), MinionSide.Enemy, WomanArcherPrefab.name);
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
            minion.Initialize(minionData, MinionSide.Player, prefab.name);
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
                minion.lastAttackTime = oneMinionObject.lastAttackTime;              
                minion.minionType = oneMinionObject.minionType;
                minionListSaveData.Minions.Add(minion);

            }


            return minionListSaveData;
        }

        public void OnLoad(MinionListSaveData minionList)
        {
            Clear();
            foreach (MinionSaveData minionData in minionList.Minions)
            {
                if (minionData.minionSide == MinionSide.Enemy)
                {
                    var prefabGameObject = Resources.Load(minionData.minionType) as GameObject;
                    Minion prefab = prefabGameObject.GetComponent<Minion>();
                    Minion minion = Instantiate(prefab, minionData.Pos,
    Data.GetEnemyFacing()) ;
                    minion.Initialize(minionData.minionData, MinionSide.Enemy, minionData.minionType);
                    EnemyMinions.Add(minion);
                }
                else
                {
                    var prefabGameObject = Resources.Load(minionData.minionType) as GameObject;
                    Minion prefab = prefabGameObject.GetComponent<Minion>();
                    Minion minion = Instantiate(prefab, minionData.Pos,
    Data.GetPlayerFacing());
                    minion.Initialize(minionData.minionData, MinionSide.Player, minionData.minionType);
                    PlayerMinions.Add(minion);

                }


            }

        }

        public void Clear()
        {
            foreach (Minion minion in EnemyMinions)
            {
                UnityEngine.Object.Destroy(minion.gameObject);
            }

            foreach (Minion minion in PlayerMinions)
            {
                UnityEngine.Object.Destroy(minion.gameObject);
            }

            EnemyMinions.Clear();
            PlayerMinions.Clear();
            
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
        public string minionType;
        public Vector2 Pos;
        public float lastAttackTime;
    }

}