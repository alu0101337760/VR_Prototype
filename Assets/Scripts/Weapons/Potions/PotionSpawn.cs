using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace VR_Prototype
{
    public class PotionSpawn : MonoBehaviour
    {
        [ContextMenu("Spawn Enemy")]
        public void SpawnPotion()
        {
            PotionManager.instance.InstantiatePotion(1, transform.position, transform.rotation);
        }
    }
}
