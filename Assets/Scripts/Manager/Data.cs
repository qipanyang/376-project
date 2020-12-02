using System;
using System.Collections.Generic;
using System.Linq;
using Minions;
using UnityEngine;

namespace Manager
{
    public class Data
    {
        public static Vector3 GetPlayerCastlePosition()
        {
            Vector3 pos = GameManager.Ctx.playerCastleObject.transform.position;
            return new Vector3(pos.x, pos.y - 1.2f, pos.z - 1);
        }

        public static Vector3 GetEnemyCastlePosition()
        {
            Vector3 pos = GameManager.Ctx.enemyCastleObject.transform.position;
            return new Vector3(pos.x, pos.y - 1.2f, pos.z - 1);
        }


        public static MinionData GetWomanArcherMinionData()
        {
            return new MinionData(500, 20, 3, 2, 5, 100);
        }
        
        public static MinionData GetWomanAttackerMinionData()
        {
            return new MinionData(500, 50, 3, 2, 2, 100);
        }
        
        public static MinionData GetElfArcherMinionData()
        {
            return new MinionData(500, 50, 3, 2, 2, 100);
        }
        
        public static MinionData GetElfAttackerMinionData()
        {
            return new MinionData(500, 50, 3, 2, 2, 100);
        }
        
        public static MinionData GetKnightPikemanMinionData()
        {
            return new MinionData(500, 50, 3, 2, 2, 100);
        }
        
        public static MinionData GetKnightWarriorMinionData()
        {
            return new MinionData(500, 50, 3, 2, 2, 100);
        }
        
        public static MinionData GetWomanWarriorMinionData()
        {
            return new MinionData(500, 50, 3, 2, 2, 100);
        }
        
        public static Quaternion GetPlayerFacing()
        {
            return Quaternion.Euler(0, 180, 0);
        }

        public static Quaternion GetEnemyFacing()
        {
            return Quaternion.identity;
        }

        public static float MinionDistance(Minion left, Minion right)
        {
            return Math.Abs(left.gameObject.transform.position.x - right.gameObject.transform.position.x);
        }

        public static Minion FindMinionToAttack(Minion target, List<Minion> minions)
        {
            if (minions.Count == 0)
            {
                return null;
            }
            
            Minion toAttack = minions.Aggregate((curMin, minion) =>
            {
                if (curMin is null || MinionDistance(curMin, target) > MinionDistance(minion, target))
                {
                    return minion;
                }
                else
                {
                    return curMin;
                }
            });

            if (toAttack is null || MinionDistance(toAttack, target) > target.minionData.Range)
            {
                return null;
            }

            return toAttack;
        }
    }
}