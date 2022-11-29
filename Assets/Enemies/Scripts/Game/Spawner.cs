using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace VR_Prototype
{
    public class Spawner : MonoBehaviour
    {
        public int id = 0;
        private Seeker[] seeker;
        private int currentTarget = 0;

        void Start()
        {
            seeker = new Seeker[EnemyPool.instance.interestPoints.Count];
            for (int i = 0; i < seeker.Length; i++)
            {
                seeker[i] = gameObject.AddComponent<Seeker>();
            }
            CalculatePath();
        }

        void CalculatePath()
        {
            Debug.Log("CalculatePath(" + id + ", " + currentTarget + ")");
            if (id == currentTarget) {
                this.currentTarget++;
                CalculatePath();
            }
            else if (currentTarget >= EnemyPool.instance.interestPoints.Count) return;
            else if (seeker[currentTarget].IsDone()) seeker[currentTarget].StartPath(transform.position, EnemyPool.instance.interestPoints[currentTarget].position, OnPathComplete);
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                EnemyPool.instance.paths.Add((id, currentTarget), p);
                currentTarget++;
                CalculatePath();
            }
        }
    }
}