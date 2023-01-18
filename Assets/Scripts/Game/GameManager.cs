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
        public int infiniteIncrement = 5;
        private int currentWaveSize = 0;
        private bool inWave = false;
        private bool gameOver = false;
        public UnityEvent OnWaveEnd;

        void Awake()
        {
            if (instance == null) instance = this;
            else Debug.LogError("More than one WaveManager in scene");
        }

        [ContextMenu("Start Wave")]
        public void StartWave() {
            if (inWave || gameOver) return;
            inWave = true;
            currentWaveSize += infiniteIncrement;
            StartCoroutine(EnemyPool.instance.SpawnWave(currentWaveSize));
            wave++;
            Debug.Log("Wave " + wave + " Started");
        }

        public void EndWave() {
            inWave = false;
            OnWaveEnd.Invoke();
            Debug.Log("Wave " + wave + " Ended");
        }

        [ContextMenu("Restart")]
        public void Restart() {
            Debug.Log("Restarting");
            wave = 0;
            currentWaveSize = 0;
            gameOver = false;
            inWave = false;
            EnemyPool.instance.Reset();
        }
        public void GameOver(bool win = false)
        {
            Debug.Log("Game Over: " + (win ? "You Win!" : "You Lose!"));
            gameOver = true;
        }
    }
}