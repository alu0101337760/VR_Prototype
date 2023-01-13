using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    public abstract class Potion : MonoBehaviour
    {
        public float radius = 1.0f;
        public float timeOfActivation = -1;
        public float effectDuration = 5;

        protected abstract void HandlePotionEffect(Collision collision);

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
            {
                this.timeOfActivation = Time.time;
                PotionManager.instance.AddUsedPotion(this);
                HandlePotionEffect(collision);
            }
        }
    }
}