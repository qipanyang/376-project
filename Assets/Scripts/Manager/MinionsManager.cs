using UnityEngine;

namespace Manager
{
    public class MinionsManager : MonoBehaviour
    {
        public Minions.Archer archerPrefab;

        internal void Start()
        {
            InvokeRepeating(nameof(SpawnEnemyMinions), 0, 1f);
        }

        public void SpawnEnemyMinions()
        {
            Vector3 enemyCastlePosition = GameManager.Ctx.enemyCastleObject.transform.position;
            archerPrefab.Initialize(100, 10, 1);
            Instantiate(archerPrefab, new Vector3(enemyCastlePosition.x, enemyCastlePosition.y - 0.5f, 0),
                Quaternion.identity);
        }
        
        public void GenerateArcher()
        {
            Vector3 playerCastlePosition = GameManager.Ctx.playerCastleObject.transform.position;
            archerPrefab.Initialize(100, 10, -1);
            Instantiate(archerPrefab, new Vector3(playerCastlePosition.x, playerCastlePosition.y - 0.5f, 0),
                Quaternion.Euler(0, 180, 0));
        }
    }
}