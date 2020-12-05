using UnityEngine;

namespace Castle
{
    public abstract class Castle: MonoBehaviour
    {
        public int Health;
        
        public AudioClip attacked;

        public void IsAttacked(int damage)
        {
            Health -= damage;
            GameManager.Instance.GetComponent<AudioSource>().PlayOneShot(attacked);
        }
    }
}