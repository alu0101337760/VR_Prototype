using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace VR_Prototype
{
    public class PotionSpawn : MonoBehaviour
    {
        public Transform spawnPoint;


        [ContextMenu("Spawn all Potions")]
        public void SpawnPotions()
        {
            PotionManager.instance.InstantiatePotion(0, spawnPoint.position , spawnPoint.rotation);
            PotionManager.instance.InstantiatePotion(1, spawnPoint.position + new Vector3(.1f, 0, 0), spawnPoint.rotation);
            PotionManager.instance.InstantiatePotion(2, spawnPoint.position + new Vector3(.2f, 0, 0), spawnPoint.rotation);
            PotionManager.instance.InstantiatePotion(3, spawnPoint.position + new Vector3(.3f, 0, 0), spawnPoint.rotation);
        }

        [ContextMenu("Spawn Potion")]
        public void SpawnPotion()
        {
            PotionManager.instance.InstantiatePotion(1, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
