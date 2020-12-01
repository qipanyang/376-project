using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace Minions
{
    public abstract class Minion : MonoBehaviour
    {
        public MinionSide minionSide;
        public MinionData minionData;
        public Rigidbody2D rb;
        public Renderer rd;
        public long lastAttackTime;
        private bool _isChangingColor;

        public void Initialize(MinionData minionData, MinionSide minionSide)
        {
            this.minionData = minionData;
            this.minionSide = minionSide;
        }

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rd = GetComponent<Renderer>();
            SetVelocity(minionData.Velocity);
        }

        private void Update()
        {
            var minionsManager = GameManager.Ctx.MinionsManager;
            var minions = minionSide == MinionSide.Enemy ? minionsManager.PlayerMinions : minionsManager.EnemyMinions;
            Minion toAttack = Data.FindMinionToAttack(this, minions);
            if (!(toAttack is null))
            {
                SetVelocity(0);
                Attack(toAttack);
            }
            else
            {
                SetVelocity(minionData.Velocity);
            }
        }

        public void Attack(Minion minion)
        {
            var now = DateTime.UtcNow.Ticks;
            if (now - lastAttackTime > minionData.AttackCdInSeconds * 10E6)
            {
                lastAttackTime = now;
                minion.IsAttacked(minionData.AttackDamage);
            }
        }

        public void IsAttacked(int damage)
        {
            minionData.Health -= damage;
            StartCoroutine(ChangeColor(Color.red));
        }

        public System.Collections.IEnumerator ChangeColor(Color newColor)
        {
            if (_isChangingColor == false)
            {
                _isChangingColor = true;
                Material material = rd.material;
                Color originalColor = material.color;
                material.color = newColor;
                yield return new WaitForSeconds(0.5f);
                material.color = originalColor;
                _isChangingColor = false;
            }
        }

        public void SetVelocity(float velocity)
        {
            rb.velocity = new Vector2(minionSide == MinionSide.Enemy ? velocity : -velocity, 0f);
        }

        public bool IsDead()
        {
            return minionData.Health <= 0;
        }

        public void DestroyGameObject()
        {
            Destroy(gameObject);
        }
    }
}