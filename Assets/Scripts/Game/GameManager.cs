using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VR_Prototype
{
    public class GameManager : MonoBehaviour
    {
        static public GameManager instance { get; private set; }
        public int wave = 0;
        public List<int> waves = new List<int>();
        public float timeBetweenWaves = 5f;
        public int infiniteIncrement = 5;
        private int currentWaveSize = 0;
        private bool infinite = false;
        private bool gameStarted = false;
        public UnityEvent OnWaveEnd;

        void Awake()
        {
            if (instance == null) instance = this;
            else Debug.LogError("More than one WaveManager in scene");
        }

        [ContextMenu("Start Game")]
        public void StartGame()
        {
            gameStarted = true;
            infinite = false;
            wave = 0;
            StartWave();
        }   

        [ContextMenu("Start Infinite Game")]
        public void StartInfiniteGame()
        {
            gameStarted = true;
            infinite = true;
            currentWaveSize += infiniteIncrement;
            StartInfiniteWave();
        }

        public void StartInfiniteWave() {
            StartCoroutine(EnemyPool.instance.SpawnWave(currentWaveSize));
            wave++;
            Debug.Log("Wave " + wave + " Started");
        }

        public void StartWave() {
            if (wave < waves.Count) {
                StartCoroutine(EnemyPool.instance.SpawnWave(waves[wave]));
                wave++;
                Debug.Log("Wave " + wave + " Started");
            }
        }

        public void GameOver(bool win = false)
        {
            Debug.Log("Game Over: " + (win ? "You Win!" : "You Lose!"));
            gameStarted = false;
        }
    }
}