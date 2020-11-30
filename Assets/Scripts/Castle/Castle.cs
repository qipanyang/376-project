using UnityEngine;

namespace Castle
{
    public abstract class Castle : MonoBehaviour
    {
        public int health;
        public int moneyRate;

        public void Initialize(int health, int moneyRate)
        {
            this.health = health;
            this.moneyRate = moneyRate;
        }
    }
}