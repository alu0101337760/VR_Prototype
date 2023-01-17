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
        public PotionNames potionName;

        private bool isTriggered = false;

        protected abstract void HandlePotionEffect(Collision collision);

        public abstract void StopPotionEffect();

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 7 && !isTriggered)
            {
                Debug.Log("Potion effect triggered");
                isTriggered = true;
                this.timeOfActivation = Time.time;                
                PotionManager.instance.AddUsedPotion(this);
                HandlePotionEffect(collision);
            }
        }
    }
}