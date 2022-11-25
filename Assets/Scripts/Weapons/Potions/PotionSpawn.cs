using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    public class PotionSpawn : MonoBehaviour
    {
        void Start()
        {
            PotionManager.instance.InstantiatePotion(0, transform.position, transform.rotation);
        }
    }
}
