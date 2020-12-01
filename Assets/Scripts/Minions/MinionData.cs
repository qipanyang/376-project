namespace Minions
{
    public class MinionData
    {
        public int Health;
        public int AttackDamage;
        public long AttackCdInSeconds;
        public int Velocity;
        public float Range;

        public MinionData(int health, int attackDamage, long attackCdInSeconds, int velocity, float range)
        {
            Health = health;
            AttackDamage = attackDamage;
            AttackCdInSeconds = attackCdInSeconds;
            Velocity = velocity;
            Range = range;
        }
    }
}