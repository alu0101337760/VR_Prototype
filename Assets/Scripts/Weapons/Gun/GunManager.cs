using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

namespace VR_Prototype
{
    public class GunManager : MonoBehaviour
    {
        public static GunManager instance;

        public float spawnTime = 1;
        public float despawnTime = 1;

        public Transform pistolSpawnerLocation;
        public Transform canonSpawnerLocation;

        public PistolBehaviour gunA;
        public PistolBehaviour gunB;
        public PotionCannon potCan;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                throw new System.Exception("There can only be one GunManager");
            }

        }

        private void Start()
        {
            gunA.transform.position = pistolSpawnerLocation.position;
            gunA.transform.rotation = pistolSpawnerLocation.rotation;

            gunB.gameObject.SetActive(false);
        }
        IEnumerator GunDespawnCorroutine(PistolBehaviour gun)
        {
            yield return new WaitForSeconds(despawnTime);
            gun.gameObject.SetActive(false);
        }

        IEnumerator GunSpawnCorroutine(PistolBehaviour gun)
        {
            yield return new WaitForSeconds(spawnTime);
            gun.gameObject.SetActive(true);
            gun.rb.isKinematic = true;
            gun.transform.position = pistolSpawnerLocation.position;
            gun.transform.rotation = pistolSpawnerLocation.rotation;
            gun.rb.isKinematic = false;
        }
        public void SpawnGun(SelectExitEventArgs args)
        {
            if (gunA.gameObject.activeSelf)
            {
                StartCoroutine(GunDespawnCorroutine(gunA));
                StartCoroutine(GunSpawnCorroutine(gunB));
            }
            else
            {
                StartCoroutine(GunDespawnCorroutine(gunB));
                StartCoroutine(GunSpawnCorroutine(gunA));
            }
        }
    }
}
