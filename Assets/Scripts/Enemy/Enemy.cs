using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    public class Enemy : Destructible
    {
        public int id = 0;
        public bool active = false;
        public bool dying = false;
        public Transform target;
        public Transform currentTarget;
        public float reach = 3f;
        public float maxSpeed = 1f;
        public float currentSpeed = 1f;
        public float maxHealth = 3f;
        public float attackDamage = 10f;
        public float attackDelay = 1f;
        public int dropChancePercentage = 20;
        private float attackTimer = 0f;
        private bool isAttacking = false;
        private EnemyMovement movement;
        public EnemyVisuals visuals { get; private set; }

        void Start()
        {
            movement = GetComponent<EnemyMovement>();
            visuals = GetComponent<EnemyVisuals>();
            gameObject.SetActive(false);
        }

        void Update() {
            if (!active) return;
            if (dying) {
                dying = !visuals.IsItDeadYet();
                if (!dying) {
                    active = false;
                }
            }
            else if (!isAttacking && currentTarget != null) movement.MovementUpdate();
            else if (isAttacking) Attack();
        }

        public void Activate()
        {
            active = true;
            dying = false;
            isAttacking = false;
            health = maxHealth;
            gameObject.SetActive(true);
            visuals.Reset();
        }

        public void SetObjective(Transform objective)
        {
            if (objective == null) return;
            currentSpeed = maxSpeed;
            target = objective;
            SetCurrentTarget(objective);
        }

        public void SetCurrentTarget(Transform objective)
        {
            if (objective == null) return;
            currentTarget = objective;
            movement.UpdatePath(objective);
            Resume();
        }

        [ContextMenu("Set Speed")]
        public void SetSpeed(float speedRatio)
        {
            currentSpeed = speedRatio * maxSpeed;
            visuals.SetAnimationSpeed(speedRatio);
        }

        public void Halt()
        {
            visuals.Stop();
            currentSpeed = 0;
        }

        public void Resume()
        {
            if (!isAttacking) {
                visuals.SetAnimationSpeed(1);
                visuals.Walk();
                currentSpeed = maxSpeed;
            }
        }

        public void OnTargetReached()
        {
            Halt();
            isAttacking = true;
            if (currentTarget == target)
            {
                OnObjectiveReached();
            }
        }

        public void OnObjectiveReached()
        {
        }

        public void OnPathInterrupted(Transform interrupter, bool offensive = false)
        {
            if (offensive) {
                SetCurrentTarget(target);
            } else {
                movement.UpdatePath(currentTarget);
            }
        }

        public void OnObjectiveComplete()
        {
            isAttacking = false;
            EnemyPool.instance.SwitchObjective(id);
        }

        private void Attack()
        {
            if (attackTimer > 0) attackTimer -= Time.deltaTime;
            else
            {
                Destructible dest = currentTarget.GetComponent<Destructible>();
                if (dest != null && dest.health > 0) {
                    visuals.Attack();
                    attackTimer = attackDelay;
                    dest.TakeDamage(attackDamage);
                }
                else {
                    OnObjectiveComplete();
                }
            }
        }

        private void DropLogic()
        {
            int rand = Random.Range(0, 100);
            if(rand > dropChancePercentage)
            {
                //TRIGGER DROP VISUALS
                rand = Random.Range(0, ItemManager.instance.itemList.itemList.Count-1);
                InventoryManager.instance.AddItem(rand);
            }
        }

        override public void Die() {
            DropLogic();
            EnemyPool.instance.KillEnemy(id);
        }

        override public void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            if (health <= 0) {
                EnemyPool.instance.KillEnemy(id);
            }
        }
    }
}