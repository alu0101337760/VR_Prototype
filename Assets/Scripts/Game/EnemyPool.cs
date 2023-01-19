using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VR_Prototype
{
    public class EnemyPool : MonoBehaviour
    {
        static public EnemyPool instance { get; private set; }

        public int poolSize = 10;
        public float spawnDelay = 1f;

        [SerializeField]
        private int livingEnemies = 0;
        private int queuedEnemies = 0;
        public GameObject enemyPrefab;
        public List<Enemy> enemies = new List<Enemy>();

        [Space]
        public List<Transform> interestPoints;
        public List<int> startSpawners = new List<int>();
        private List<int> spawners;
        private List<int> objectives = new List<int>();

        private bool spawningWave = false;

        void Start()
        {
            if (instance == null) instance = this;
            else
            {
                Debug.LogError("More than one EnemyPool in scene");
                return;
            }
            enemies = new List<Enemy>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity, transform);
                Enemy behauviour = enemy.GetComponent<Enemy>();
                behauviour.id = i;
                enemies.Add(behauviour);
            }
            for (int i = 0; i < interestPoints.Count; i++)
            {
                Objective obj = interestPoints[i].gameObject.AddComponent<Objective>();
                obj.id = i;
                obj.maxHealth = 100 + 50 * i;
                obj.Reset();
            }
            spawners = new List<int>(startSpawners);
            if (spawners.Count == 0) spawners.Add(0);
            for (int i = 0; i < interestPoints.Count; i++)
            {
                if (!spawners.Contains(i)) objectives.Add(i);
            }
            for (int i = 0; i < spawners.Count; i++) interestPoints[spawners[i]].GetComponent<Objective>().Die();
        }

        public void Reset() {
            if (livingEnemies > 0) KillAllEnemies();
            spawners = new List<int>(startSpawners);
            if (spawners.Count == 0) spawners.Add(0);
            for (int i = 0; i < interestPoints.Count; i++)
            {
                if (!spawners.Contains(i)) {
                    objectives.Add(i);
                    interestPoints[i].GetComponent<Objective>().Reset();
                }
            }
        }

        [ContextMenu("Halve Speed")]
        public void HalveSpeed() { SlowDownEnemies(0.5f); }
        [ContextMenu("Restore Speed")]
        public void RestoreSpeed() { SlowDownEnemies(1f); }
        public void SlowDownEnemies(float slowFactor)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].active) enemies[i].SetSpeed(slowFactor);
            }
        }

        public IEnumerator SpawnWave(int enemyNumber)
        {
            queuedEnemies = enemyNumber;
            spawningWave = true;
            for (int i = 0; i < enemyNumber; i++)
            {
                yield return new WaitForSeconds(spawnDelay);
                if (!SpawnEnemy()) i--;
            }
            spawningWave = false;
        }

        [ContextMenu("Spawn Enemy")]
        private bool SpawnEnemy()
        {
            if (objectives.Count == 0) return false;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].active)
                {
                    int spawner = spawners[Random.Range(0, spawners.Count)];
                    int objective = objectives[Random.Range(0, objectives.Count)];
                    Debug.Log("Spawning enemy at " + spawner + " with objective " + objective);
                    enemies[i].transform.position = interestPoints[spawner].position;
                    enemies[i].Activate();
                    enemies[i].SetObjective(interestPoints[objective]);
                    livingEnemies++;
                    queuedEnemies--;
                    return true;
                }
            }
            return false;
        }

        public void Switch2Spawner(int id)
        {
            if (id < 0 || id >= interestPoints.Count) return;
            if (objectives.Contains(id)) objectives.Remove(id);
            if (!spawners.Contains(id)) spawners.Add(id);
            if (objectives.Count == 0) GameManager.instance.GameOver();
        }

        public void SwitchObjective(int id)
        {
            if (objectives.Count == 0) return;
            int objective = objectives[Random.Range(0, objectives.Count)];
            enemies[id].SetObjective(interestPoints[objective]);
        }

        public void EnemyHit(int id, float damage = 1f)
        {
            enemies[id].TakeDamage(damage);
        }

        public void EnemyHit(int[] ids, float damage = 1f)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                enemies[ids[i]].TakeDamage(damage);
            }
        }

        public void KillEnemy(int id)
        {
            enemies[id].dying = true;
            enemies[id].visuals.Die();
            livingEnemies--;
            if (livingEnemies <= 0 && queuedEnemies <= 0 && !spawningWave) GameManager.instance.EndWave();
        }

        public void KillEnemy(int[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                KillEnemy(ids[i]);
            }
        }

        [ContextMenu("Kill All Enemies")]
        public void KillAllEnemies()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].active && !enemies[i].dying) KillEnemy(i);
            }
        }

        public Enemy[] GetLivingEnemies()
        {
            List<Enemy> activeEnemies = new List<Enemy>();
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].active && !enemies[i].dying)
                {
                    activeEnemies.Add(enemies[i]);
                };
            }
            return activeEnemies.ToArray();
        }
    }
}