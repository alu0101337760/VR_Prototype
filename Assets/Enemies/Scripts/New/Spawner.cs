using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace VR_Prototype
{
    public class Spawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public Transform[] targetPoints;
        private Seeker seeker;

        void Start()
        {
            seeker = GetComponent<Seeker>();
            seeker.StartPath(transform.position, targetPoints[Random.Range(0, targetPoints.Length)].position, OnPathComplete);
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                SpawnEnemy(p);
            }
        }

        public void SpawnEnemy(Path p = null)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyMovement>().target = targetPoints[Random.Range(0, targetPoints.Length)].position;
            enemy.GetComponent<EnemyMovement>().path = p;
            enemy.GetComponent<Enemy>().SetObjective(targetPoints[Random.Range(0, targetPoints.Length)]);
            
        }
    }
}