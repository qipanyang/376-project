using System;
using UnityEngine;

namespace Manager
{
    public class GoldManager : MonoBehaviour
    {
        public int gold;
        public int goldRate;

        private void Start()
        {
            InvokeRepeating(nameof(IncreaseGold), 0, 0.1f);
        }

        private void IncreaseGold()
        {
            gold += goldRate;
        }

        public bool DecreaseGold(int amount)
        {
            if (gold - amount >= 0)
            {
                gold -= amount;
                return true;
            }

            return false;
        }

        private void Update()
        {
            
            GameManager.Ctx.TextManager.UpdateGold(gold);
        }
    }
}