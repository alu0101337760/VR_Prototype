using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VR_PrototypeOld {
    public class EnemyPool : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public Transform[] targetPoints;
        private Enemy[] enemies;
        public int poolSize = 10;

        void Awake()
        {
            enemies = new Enemy[poolSize];
            for (int i = 0; i < poolSize; i++)
            {
                enemies[i] = Instantiate(enemyPrefab, transform).GetComponent<Enemy>();
            }
        }

        public void SpawnEnemy()
        {
            for (int i = 0; i < poolSize; i++)
            {
                if (!enemies[i].active)
                {
                    enemies[i].HP = 100;
                    enemies[i].transform.position = transform.position;
                    //enemies[i].UpdatePath(targetPoints[Random.Range(0, targetPoints.Length)]);
                    enemies[i].active = true;
                    enemies[i].gameObject.SetActive(true);
                    break;
                }
            }
        }
    }
}
