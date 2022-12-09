using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    public class PotionA : Potion
    {
        protected override void HandlePotionEffect()
        {
            Debug.Log("Potion Effect triggered yay");
        }

        private void Start()
        {
            triggerZone = this.GetComponent<SphereCollider>();
            this.effectDuration = 5;
        }


    }
}
