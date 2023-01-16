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
        public float unshotDespawnTime = 10;

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

            potCan.transform.position = canonSpawnerLocation.position;
            potCan.transform.rotation = canonSpawnerLocation.rotation;
        }
        IEnumerator PistolDespawnCorroutine(PistolBehaviour gun)
        {
            if (gun.alreadyShot)
            {
                yield return new WaitForSeconds(despawnTime);
            }
            else
            {
                yield return new WaitForSeconds(unshotDespawnTime);
            }
            gun.gameObject.SetActive(false);
        }

        IEnumerator PistolSpawnCorroutine(PistolBehaviour gun)
        {
            if(!(gunA.isActiveAndEnabled && gunB.isActiveAndEnabled))
            {
                yield return new WaitForSeconds(spawnTime);
                gun.gameObject.SetActive(true);
                gun.rb.isKinematic = true;
                gun.transform.position = pistolSpawnerLocation.position;
                gun.transform.rotation = pistolSpawnerLocation.rotation;
                gun.rb.isKinematic = false;
            }            
        }

        public void ResetCannon(SelectExitEventArgs args)
        {
            potCan.transform.position = canonSpawnerLocation.position;
            potCan.transform.rotation = canonSpawnerLocation.rotation;
        }

        public void SpawnPistol(SelectExitEventArgs args)
        {
            if (gunA.gameObject.activeSelf)
            {
                StartCoroutine(PistolDespawnCorroutine(gunA));
                StartCoroutine(PistolSpawnCorroutine(gunB));
            }
            else
            {
                StartCoroutine(PistolDespawnCorroutine(gunB));
                StartCoroutine(PistolSpawnCorroutine(gunA));
            }
        }
    }
}
