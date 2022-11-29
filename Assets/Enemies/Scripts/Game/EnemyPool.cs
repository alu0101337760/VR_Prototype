using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace VR_Prototype
{
    public class EnemyPool : MonoBehaviour
    {
        static public EnemyPool instance { get; private set; }
        public GameObject enemyPrefab;
        public int poolSize = 10;
        public List<int> spawners = new List<int>();
        private List<int> objectives = new List<int>();
        public List<Transform> interestPoints;
        public Dictionary<(int,int), Path> paths = new Dictionary<(int, int), Path>();
        private List<GameObject> enemies;
        private int remainingEnemies = 0;
        private void Start()
        {
            if (instance == null) instance = this;
            else Debug.LogError("More than one EnemyPool in scene");
            enemies = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity, transform);
                enemies.Add(enemy);
            }
            for (int i = 0; i < interestPoints.Count; i++)
            {
                interestPoints[i].gameObject.AddComponent<Spawner>().id = i;
            }
            if (spawners.Count == 0) spawners.Add(0);
            for (int i = 0; i < interestPoints.Count; i++)
            {
                if (!spawners.Contains(i)) objectives.Add(i);
            }
        }

        public void SpawnEnemy(int objective) {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].activeInHierarchy)
                {
                    int spawner = spawners[Random.Range(0, spawners.Count)];
                    enemies[i].transform.position = interestPoints[spawner].position;
                    enemies[i].SetActive(true);
                    remainingEnemies++;
                    enemies[i].GetComponent<Enemy>().SetObjective(interestPoints[objective], paths[(spawner, objective)]);
                    return;
                }
            }
        }

        public IEnumerator SpawnWave(int wave)
        {
            if (WaveManager.instance.waves[wave].x > 0) {
                for (int i = 0; i < WaveManager.instance.waves[wave].x; i++)
                {
                    SpawnEnemy(objectives[Random.Range(0, objectives.Count)]);
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }

        public void TurnObjectiveIntoSpawner(int id) {
            objectives.Remove(id);
            spawners.Add(id);
        }

        public void SwitchObjective(Enemy enemy) {
            if (objectives.Count > 0)
            {
                int objective = objectives[Random.Range(0, objectives.Count)];
                enemy.SetObjective(interestPoints[objective]);
            }
        }
    }
}