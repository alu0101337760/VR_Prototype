using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VR_Prototype {
    public class WaveManager : MonoBehaviour
    {
        private EnemyPool enemyPool;
        public float spawnRate = 1;
        private float spawnTimer = 0;
        public int enemiesPerWave = 10;
        public float timeBetweenWaves = 5;
        private float waveTimer = 0;

        void Start()
        {
            enemyPool = GetComponent<EnemyPool>();
        }

        void Update()
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate && enemiesPerWave > 0)
            {
                enemyPool.SpawnEnemy();
                spawnTimer = 0;
                enemiesPerWave--;
            }
            if (enemiesPerWave == 0) {
                waveTimer += Time.deltaTime;
                if (waveTimer >= timeBetweenWaves) {
                    enemiesPerWave = 10;
                    waveTimer = 0;
                }
            }
        }
    }
}
