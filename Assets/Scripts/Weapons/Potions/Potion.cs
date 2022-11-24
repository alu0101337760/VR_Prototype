using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    public abstract class Potion : MonoBehaviour
    {
        protected SphereCollider triggerZone;
        public float timeOfActivation = -1;
        public float effectDuration = 5;

        protected abstract void HandlePotionEffect();

        private void OnCollisionEnter(Collision collision)
        {
            this.timeOfActivation = Time.time;
            PotionManager.instance.AddUsedPotion(this);
            HandlePotionEffect();
        }
    }
}