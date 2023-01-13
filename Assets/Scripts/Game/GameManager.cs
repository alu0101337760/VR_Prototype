using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    public class GameManager : MonoBehaviour
    {
        static public GameManager instance { get; private set; }
        public int wave = 0;
        public List<int> waves = new List<int>();
        public float timeBetweenWaves = 5f;
        void Awake()
        {
            if (instance == null) instance = this;
            else Debug.LogError("More than one WaveManager in scene");
        }

        [ContextMenu("Start Game")]
        void StartGame()
        {
            wave = 0;
            StartWave();
        }

        void StartWave() {
            if (wave < waves.Count) {
                StartCoroutine(EnemyPool.instance.SpawnWave(waves[wave]));
                wave++;
                Debug.Log("Wave " + wave + " Started");
            }
        }

        public IEnumerator OnWaveEnded()
        {
            Debug.Log("Wave " + wave + "Ended");
            yield return new WaitForSeconds(timeBetweenWaves);
            if (wave < waves.Count) StartWave();
            else {
                GameOver(true);
            }
        }

        public void GameOver(bool win = false)
        {
            Debug.Log("Game Over: " + (win ? "You Win!" : "You Lose!"));
        }
    }
}