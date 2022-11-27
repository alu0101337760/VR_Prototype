using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace VR_Prototype
{
    public class EnemyMovement : MonoBehaviour
    {
        public float speed = 3f;
        public float nextWaypointDistance = .5f;
        public float lastWaypointDistance = 2f;
        public Vector3 target;
        public Path path;
        private int currentWaypoint = 0;
        public bool reachedEndOfPath = false;
        private Seeker seeker;
        private Enemy behaviour;

        private void Start()
        {
            seeker = GetComponent<Seeker>();
            behaviour = GetComponent<Enemy>();
        }

        public void UpdatePath(Vector3 newTarget)
        {
            target = newTarget;
            if (seeker.IsDone()) seeker.StartPath(transform.position, target, OnPathComplete);
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
            if (reachedEndOfPath) behaviour.OnPathComplete();
            if (path == null) return;
            if (currentWaypoint >= path.vectorPath.Count || Vector3.Distance(transform.position, target) < lastWaypointDistance)
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
            float distance = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }
    }
}