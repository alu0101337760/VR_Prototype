using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    public class Enemy : Destructible
    {
        public int id = 0;
        public bool active = false;
        public Transform target;
        public Transform currentTarget;
        public float reach = 3f;
        public float speed = 1f;
        public float currentSpeed = 1f;

        public float attackDamage = 10f;
        public float attackDelay = 1f;
        private float attackTimer = 0f;
        private bool isAttacking = false;
        private EnemyMovement movement;
        private EnemyVisuals visuals;

        void Start()
        {
            movement = GetComponent<EnemyMovement>();
            visuals = GetComponent<EnemyVisuals>();
            gameObject.SetActive(false);
        }

        void Update() {
            if (!isAttacking && currentTarget != null) movement.MovementUpdate();
            else if (isAttacking) Attack();
        }

        public void Activate()
        {
            active = true;
            gameObject.SetActive(true);
            visuals.Reset();
        }

        public void SetObjective(Transform objective)
        {
            if (objective == null) return;
            currentSpeed = speed;
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

        public void Halt()
        {
            visuals.Stop();
            currentSpeed = 0;
        }

        public void Resume()
        {
            if (!isAttacking) {
                visuals.Walk();
                currentSpeed = speed;
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

        public void OnPathInterrupted(Transform interrupter)
        {
            if (interrupter == currentTarget) {
                SetCurrentTarget(target);
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

        override public void Die() {
            visuals.Die();
            active = false;
        }
    }
}