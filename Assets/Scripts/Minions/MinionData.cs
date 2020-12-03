namespace Minions
{
    public class MinionData
    {
        public int Health;
        public int AttackDamage;
        public long AttackCdInSeconds;
        public int Velocity;
        public float Range;
        public int Price;

        public MinionData(int health, int attackDamage, long attackCdInSeconds, int velocity, float range, int price)
        {
            Health = health;
            AttackDamage = attackDamage;
            AttackCdInSeconds = attackCdInSeconds;
            Velocity = velocity;
            Range = range;
            Price = price;
        }

        private MinionData()
        {

        }
    }
}