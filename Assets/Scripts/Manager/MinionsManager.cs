using System.Collections;
using System.Collections.Generic;
using Minions;
using UnityEngine;

namespace Manager
{
    public class MinionsManager : MonoBehaviour
    {
        public Archer archerPrefab;
        public List<Minion> minionArray = new List<Minion>();

        internal void Start()
        {
            InvokeRepeating(nameof(SpawnEnemyMinions), 0, 1f);
        }

        public void SpawnEnemyMinions()
        {
            Vector3 enemyCastlePosition = GameManager.Ctx.enemyCastleObject.transform.position;
            archerPrefab.Initialize(100, 10, 1);
            Minion minion = Instantiate(archerPrefab, new Vector3(enemyCastlePosition.x, enemyCastlePosition.y - 1.2f, 0),
                Quaternion.identity);
            minionArray.Add(minion);
            
        }
        
        public void GenerateArcher()
        {
            Vector3 playerCastlePosition = GameManager.Ctx.playerCastleObject.transform.position;
            archerPrefab.Initialize(100, 10, -1);
            Instantiate(archerPrefab, new Vector3(playerCastlePosition.x, playerCastlePosition.y - 1.2f, 0),
                Quaternion.Euler(0, 180, 0));
        }
    }
}