using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VR_Prototype {
    public class EnemyPool : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public Transform[] targetPoints;
        private GameObject[] enemies;
        public int poolSize = 10;

        void Start()
        {
            enemies = new GameObject[poolSize];
            for (int i = 0; i < poolSize; i++)
            {
                enemies[i] = Instantiate(enemyPrefab, transform);
                enemies[i].SetActive(false);
            }
        }

        public void SpawnEnemy()
        {
            for (int i = 0; i < poolSize; i++)
            {
                if (!enemies[i].activeInHierarchy)
                {
                    enemies[i].GetComponent<Enemy>().HP = 100;
                    enemies[i].SetActive(true);
                    enemies[i].transform.position = transform.position;
                    enemies[i].GetComponent<Enemy>().target = targetPoints[Random.Range(0, targetPoints.Length)];
                    break;
                }
            }
        }
    }
}
