using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VR_Prototype
{
    public class EnemyPool : MonoBehaviour
    {
        static public EnemyPool instance { get; private set; }

        public int poolSize = 10;
        public GameObject enemyPrefab;
        public List<Enemy> enemies = new List<Enemy>();

        [Space]
        public List<Transform> interestPoints;
        public List<int> spawners = new List<int>();
        private List<int> objectives = new List<int>();

        void Start()
        {
            if (instance == null) instance = this;
            else Debug.LogError("More than one EnemyPool in scene");
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
                obj.health = 400;
            }
            if (spawners.Count == 0) spawners.Add(0);
            for (int i = 0; i < interestPoints.Count; i++)
            {
                if (!spawners.Contains(i)) objectives.Add(i);
            }
        }

        public IEnumerator SpawnWave(int wave)
        {
            yield return null;
        }

        [ContextMenu("Spawn Enemy")]
        private void SpawnEnemy()
        {
            if (objectives.Count == 0) return;
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
                    return;
                }
            }
        }

        public void Switch2Spawner(int id)
        {
            if (id < 0 || id >= interestPoints.Count) return;
            if (objectives.Contains(id)) objectives.Remove(id);
            if (!spawners.Contains(id)) spawners.Add(id);
        }

        public void SwitchObjective(int id)
        {
            if (objectives.Count == 0) return;
            int objective = objectives[Random.Range(0, objectives.Count)];
            enemies[id].SetObjective(interestPoints[objective]);
        }

        public void EnemyHit(int id)
        {
            throw new System.NotImplementedException();
        }

        public void EnemyHit(int[] ids )
        {
            throw new System.NotImplementedException();
            
            }
        public void KillEnemy(int id)
        {
            enemies[id].Die();
        }

        public void KillEnemy(int[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                KillEnemy(ids[i]);
            }
        }

        public void KillAllEnemies()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].active) enemies[i].Die();
            }
        }
    }
}