using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace VR_Prototype
{
    public class MeleeEnemy : Enemy
    {
        public float attackDamage = 10f;
        public float attackDelay = 1f;
        private float attackTimer = 0f;
        public bool isAttacking = false;
        public bool isDancing = false;

        public override void OnObjectiveComplete() {
            isAttacking = false;
            if (currentTarget != target) {
                currentTarget = target;
                enemyMovement.UpdatePath(currentTarget.position);
            }
            else isDancing = true;
        }

        public override void OnPathInterrupted(Transform interrupter) {
            currentTarget = interrupter;
            enemyMovement.UpdatePath(currentTarget.position);
        }
        public override void OnPathComplete() {
            Halt();
            isAttacking = true;
        }

        public void Update() {
            if (isAttacking) Attack();
            else if (isDancing) Dance();
            else 
            {
                    
            };
        }

        private void Attack()
        {
            if (attackTimer > 0) attackTimer -= Time.deltaTime;
            else
            {
                attackTimer = attackDelay;
                Destructible dest = currentTarget.GetComponent<Destructible>();
                if (dest != null && dest.enabled) dest.TakeDamage(attackDamage);
                else {
                    OnObjectiveComplete();
                }
            }
        }

        private void Dance()
        {
            transform.Rotate(0, 20*Time.deltaTime, 0);
        }
    }
}