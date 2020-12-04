using UnityEngine;

namespace Castle
{
    public abstract class Castle: MonoBehaviour
    {
        public int Health;

        public void IsAttacked(int damage)
        {
            Health -= damage;
        }
    }
}