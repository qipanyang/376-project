using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Minions;
using UnityEngine;
using Random = UnityEngine.Random;

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
            InvokeRepeating(nameof(SpawnEnemyMinions), 0, 0.5f);
        }

        public void SpawnEnemyMinions()
        {
            if (EnemyMinions.Count <= 10)
            {
                var nn = Random.Range(0, 7);
                switch (nn)
                {
                    case 0:
                        GenerateEnemyMinion(Data.GetWomanArcherMinionData(), WomanArcherPrefab);
                        break;
                    case 1:
                        GenerateEnemyMinion(Data.GetWomanAttackerMinionData(), WomanAttackerPrefab);
                        break;
                    case 2:
                        GenerateEnemyMinion(Data.GetWomanWarriorMinionData(), WomanWarrior);
                        break;
                    case 3:
                        GenerateEnemyMinion(Data.GetElfArcherMinionData(), ElfArcher);
                        break;
                    case 4:
                        GenerateEnemyMinion(Data.GetElfArcherMinionData(), ElfArcher);
                        break;
                    case 5:
                        GenerateEnemyMinion(Data.GetKnightPikemanMinionData(), KnightPikeman);
                        break;
                    case 6:
                        GenerateEnemyMinion(Data.GetKnightWarriorMinionData(), KnightWarrior);
                        break;
                }
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

        private void GenerateEnemyMinion(MinionData minionData, Minion prefab)
        {
            Minion minion = Instantiate(prefab, Data.GetEnemyCastlePosition(),
                Data.GetEnemyFacing());
            minion.Initialize(minionData, MinionSide.Enemy, prefab.name);
            EnemyMinions.Add(minion);
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
                    Vector3 castlePos = GameManager.Ctx.enemyCastleObject.transform.position;
                    var pos = new Vector3(minionData.Pos.x, minionData.Pos.y, castlePos.z - 1);
                    Minion prefab = prefabGameObject.GetComponent<Minion>();
                    Minion minion = Instantiate(prefab, pos,
    Data.GetEnemyFacing()) ;
                    minion.Initialize(minionData.minionData, MinionSide.Enemy, minionData.minionType);
                    EnemyMinions.Add(minion);
                }
                else
                {
                    var prefabGameObject = Resources.Load(minionData.minionType) as GameObject;
                    Vector3 castlePos = GameManager.Ctx.playerCastleObject.transform.position;
                    var pos = new Vector3(minionData.Pos.x, minionData.Pos.y, castlePos.z - 1);
                    Minion prefab = prefabGameObject.GetComponent<Minion>();
                    Minion minion = Instantiate(prefab, pos,
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