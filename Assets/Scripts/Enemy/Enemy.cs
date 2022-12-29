using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace VR_Prototype
{
    public abstract class Enemy : MonoBehaviour
    {
        public float HP = 100;
        public float reach = 3f;
        public float speed = 1f;
        public float currentSpeed = 1f;
        public bool isDead = false;
        public Transform target;
        protected Transform currentTarget;
        protected EnemyMovement enemyMovement;
        protected virtual void Start()
        {
            Debug.Log("Enemy Start");
            currentSpeed = 0;
            enemyMovement = GetComponent<EnemyMovement>();
            gameObject.SetActive(false);
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
            currentSpeed = 0;
        }

        public void Resume()
        {
            currentSpeed = speed;
        }

        public virtual void SetObjective(Transform objective, Path path = null)
        {
            currentSpeed = speed;
            target = objective;
            currentTarget = objective;
            if (path == null) enemyMovement.UpdatePath(objective.position);
            else enemyMovement.UpdatePath(objective.position, path);
        }

        public abstract void OnObjectiveComplete();
        public abstract void OnPathInterrupted(Transform interrupter);
        public virtual void OnPathComplete() {
            Halt();
            if (EnemyPool.instance.spawners.Contains(target.GetComponent<Spawner>().id)) EnemyPool.instance.SwitchObjective(this);
        }
    }
}