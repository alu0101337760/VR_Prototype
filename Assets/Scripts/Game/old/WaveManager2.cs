using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    public class WaveManager2 : MonoBehaviour
    {
        static public WaveManager2 instance { get; private set; }
        public int wave = 0;
        public List<Vector3Int> waves = new List<Vector3Int>();
        public float timeBetweenWaves = 3f;
        private float waveTimer = 0f;
        void Awake()
        {
            if (instance == null) instance = this;
            else Debug.LogError("More than one WaveManager in scene");
        }

        void StartWave() {
            if (wave < waves.Count) {
                StartCoroutine(EnemyPool.instance.SpawnWave(wave));
                wave++;
            }
        }
        void Update()
        {
            waveTimer += Time.deltaTime;
            if (waveTimer > timeBetweenWaves) {
                StartWave();
                waveTimer = 0f;
            }
        }
    }
}