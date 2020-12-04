using System.Collections;
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
        public string minionType;
        public float lastAttackTime;
        private bool _isChangingColor; // don't save
        private bool _isRotating; // don't save
        
        public Animator animator; // animation

        public void Initialize(MinionData minionData, MinionSide minionSide, string prefabName)
        {
            this.minionData = minionData;
            this.minionSide = minionSide;
            this.minionType = prefabName;
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
                animator.SetBool("Attack", true); // animation
                SetVelocity(0);
                Attack(toAttack);
            }
            else if (CheckCanAttackTower())
            {
                animator.SetBool("Attack", true); // animation
                SetVelocity(0);
                AttackCastle();
            }
            else
            {
                animator.SetBool("Attack", false); // animation
                SetVelocity(minionData.Velocity);
            }
            
            
        }

        private bool CheckCanAttackTower()
        {
            if (minionSide == MinionSide.Enemy)
            {
                return rb.position.x + minionData.Range >= Data.GetPlayerCastlePosition().x;
            }
            else
            {
                return rb.position.x - minionData.Range <= Data.GetEnemyCastlePosition().x;
            }
        }

        private bool CanAttack()
        {
            var now = Time.time;
            if (now - lastAttackTime > minionData.AttackCdInSeconds)
            {
                lastAttackTime = now;
                //StartCoroutine(RotateMinion()); // animation
                return true;
            }
            return false;
        }

        public void Attack(Minion minion)
        {
            
            if (CanAttack())
            {
                beforeAttack();
                minion.IsAttacked(minionData.AttackDamage);
            }
        }

        public void AttackCastle()
        {
            if (CanAttack())
            {
                beforeAttack();
                GameManager ctx = GameManager.Ctx;
                if (minionSide == MinionSide.Enemy)
                {
                    ctx.PlayerCastle.IsAttacked(minionData.AttackDamage);
                }
                else
                {
                    ctx.EnemyCastle.IsAttacked(minionData.AttackDamage);
                }
            }
        }

        public void IsAttacked(int damage)
        {
            minionData.Health -= damage;
            StartCoroutine(ChangeColor(Color.red));
        }

        public IEnumerator RotateMinion()
        {
            if (_isRotating == false)
            {
                _isRotating = true;
                rb.transform.Rotate(0, 0, -15, Space.Self);
                yield return new WaitForSeconds(0.5f);
                rb.transform.Rotate(0, 0, 15, Space.Self);
                _isRotating = false;
            }
            
        }

        public IEnumerator ChangeColor(Color newColor)
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

        private IEnumerator _DestroyGameObject()
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }

        public void DestroyGameObject()
        {
            animator.SetBool("Die", true); // animation
            StartCoroutine(_DestroyGameObject());
        }

        public abstract void beforeAttack();

    }
}