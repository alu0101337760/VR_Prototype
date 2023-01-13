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
        public float despawnTime =1;
        public Transform spawnerLocation;
        public GunBehaviour gunA;
        public GunBehaviour gunB;
        
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
            gunA.transform.position = spawnerLocation.position;
            gunA.transform.rotation = spawnerLocation.rotation;
            
            gunB.gameObject.SetActive(false);            
        }
        IEnumerator GunDespawnCorroutine(GunBehaviour gun)
        {
            yield return new WaitForSeconds(despawnTime);
            gun.gameObject.SetActive(false);
        }
        
        IEnumerator GunSpawnCorroutine(GunBehaviour gun)
        {
            yield return new WaitForSeconds(spawnTime);
            gun.gameObject.SetActive(true);
            gun.rb.isKinematic = true;
            gun.transform.position = spawnerLocation.position;
            gun.transform.rotation = spawnerLocation.rotation;
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
