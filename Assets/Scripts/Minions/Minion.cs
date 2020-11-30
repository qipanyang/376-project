using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minions
{
    public abstract class Minion : MonoBehaviour
    {
        public int health;
        public int attack;
        public int speed;

        public void Initialize(int health, int attack, int speed)
        {
            this.health = health;
            this.attack = attack;
            this.speed = speed;
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}
