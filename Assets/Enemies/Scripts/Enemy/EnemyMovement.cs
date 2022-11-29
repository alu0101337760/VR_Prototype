using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace VR_Prototype
{
    public class EnemyMovement : MonoBehaviour
    {
        public float nextWaypointDistance = .5f;
        public Vector3 target;
        public Path path;
        private int currentWaypoint = 0;
        public bool reachedEndOfPath = false;
        private Seeker seeker;
        private Enemy behaviour;

        private void Start()
        {
            Debug.Log("EnemyMovement Start");
            seeker = GetComponent<Seeker>();
            behaviour = GetComponent<Enemy>();
        }

        public void UpdatePath(Vector3 newTarget, Path path = null)
        {
            target = newTarget;
            if (path == null) seeker.StartPath(transform.position, target, OnPathComplete);
            else OnPathComplete(path);
        }


        void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWaypoint = 0;
                reachedEndOfPath = false;
            }
        }

        void Update()
        {
            if (path == null || behaviour.currentSpeed == 0) return;
            if (reachedEndOfPath) behaviour.OnPathComplete();
            if (currentWaypoint >= path.vectorPath.Count || Vector3.Distance(transform.position, target) < behaviour.reach)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }
            Vector3 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            transform.Translate(direction * behaviour.currentSpeed * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }
    }
}