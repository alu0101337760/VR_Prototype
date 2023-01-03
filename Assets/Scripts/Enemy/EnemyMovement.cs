using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace VR_Prototype
{
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(Enemy))]
    public class EnemyMovement : MonoBehaviour
    {
        private Enemy behaviour;
        [HideInInspector]
        public bool targetReached = false;
        private Vector3 target;
        public Path path;
        private Seeker seeker;
        private int currentWaypoint = 0;
        private float nextWaypointDistance = .5f;
        void Awake()
        {
            seeker = GetComponent<Seeker>();
            behaviour = GetComponent<Enemy>();
        }

        // Update is called once per frame
        public void UpdatePath(Transform newTarget)
        {
            target = newTarget.position;
            seeker.StartPath(transform.position, target, OnPathComplete);
        }

        void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWaypoint = 0;
                targetReached = false;
            }
        }

        public void MovementUpdate() {
            if (path == null || behaviour.currentSpeed == 0) return;
            if (targetReached) {
                behaviour.OnTargetReached();
                return;
            }
            if (currentWaypoint >= path.vectorPath.Count || Vector3.Distance(transform.position, target) < behaviour.reach)
            {
                targetReached = true;
                return;
            }
            else
            {
                targetReached = false;
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