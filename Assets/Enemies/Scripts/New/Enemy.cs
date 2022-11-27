using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace VR_Prototype
{
    public abstract class Enemy : MonoBehaviour
    {
        public float HP = 100;
        public bool isDead = false;
        protected Transform target;
        protected Transform currentTarget;
        protected EnemyMovement enemyMovement;
        protected virtual void Awake()
        {
            enemyMovement = GetComponent<EnemyMovement>();
        }
        
        public virtual void Die()
        {
            isDead = true;
            gameObject.SetActive(false);
        }

        public void TakeDamage(float damage)
        {
            HP -= damage;
            if (HP <= 0)
            {
                Die();
            }
        }
        
        public void Halt()
        {
            enemyMovement.enabled = false;
        }

        public void Resume()
        {
            enemyMovement.enabled = true;
        }

        public virtual void SetObjective(Transform objective)
        {
            target = objective;
            currentTarget = objective;
            //enemyMovement.UpdatePath(currentTarget.position);
        }

        public abstract void OnObjectiveComplete();
        public abstract void OnPathInterrupted(Transform interrupter);
        public abstract void OnPathComplete();
    }
}