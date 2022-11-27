using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace VR_PrototypeOld
{
    public abstract class Enemy : MonoBehaviour
    {
        public float HP = 100;
        public float speed = 1f;
        public float LifeTime = 10;
        public Vector3 target;
        public float nextWaypointDistance = 3f;
        public bool active = false;
        private Path path;
        private int currentWaypoint = 0;
        private bool reachedEndOfPath = false;
        private Seeker seeker;
        private Renderer rend;

        void Start()
        {
            seeker = GetComponent<Seeker>();
            rend = GetComponentInChildren<Renderer>();
            gameObject.SetActive(false);
        }

        public void UpdatePath(Vector3 newTarget = default(Vector3))
        {
            if (newTarget != default(Vector3)) target = newTarget;
            if (seeker.IsDone()) seeker.StartPath(transform.position, target, OnPathComplete);
        }
        
        void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
            //gameObject.SetActive(true);
        }

        void Update()
        {
            rend.material.color = Color.Lerp(Color.black, Color.white, HP / 100);
            HP -= LifeTime == 0 ? 0 : 100*Time.deltaTime/LifeTime;
            if (HP <= 0) Die();
            if (reachedEndOfPath) Attack();
            if (path == null) return;
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }
            Vector3 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }

        public void TakeDamage(float damage)
        {
            HP -= damage;
            if (HP <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            active = false;
            gameObject.SetActive(false);
        }

        protected abstract void Attack();
    }
}