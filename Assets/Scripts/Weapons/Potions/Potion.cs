using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    public class Potion : MonoBehaviour
    {
        protected SphereCollider triggerZone;
        public float timeOfActivation = -1;
        public float effectDuration = 5;

        private void Start()
        {
            triggerZone = this.GetComponent<SphereCollider>();
            this.effectDuration = 5;
        }

        private void OnCollisionEnter(Collision collision)
        {
            this.timeOfActivation = Time.time;
            PotionManager.instance.AddPotionActivation(this);
        }
    }
}