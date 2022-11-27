using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VR_PrototypeOld {
    public class WaveManager : MonoBehaviour
    {
        private EnemyPool enemyPool;
        public float spawnRate = 1;
        private float spawnTimer = 0;
        public int enemiesPerWave = 10;
        private int remainingEnemies = 0;
        public float timeBetweenWaves = 5;
        private float waveTimer = 0;

        void Start()
        {
            enemyPool = GetComponent<EnemyPool>();
        }

        void Update()
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate && remainingEnemies > 0)
            {
                enemyPool.SpawnEnemy();
                spawnTimer = 0;
                remainingEnemies--;
            }
            if (remainingEnemies == 0) {
                waveTimer += Time.deltaTime;
                if (waveTimer >= timeBetweenWaves) {
                    remainingEnemies = enemiesPerWave;
                    waveTimer = 0;
                }
            }
        }
    }
}
