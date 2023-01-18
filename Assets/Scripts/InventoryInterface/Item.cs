using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace VR_Prototype {
    [RequireComponent(typeof(XRGrabInteractable))]
    public class Item : MonoBehaviour
    {
        [HideInInspector]
        public ItemDispenser dispenser;
        // [HideInInspector]
        public bool isGrabbed = false;
        // [HideInInspector]
        public bool DespawnProtection = false;
        private int lifetime;
        private bool skipLifetime = false;
        private Rigidbody rb;

        protected virtual void Start() {
            rb = GetComponent<Rigidbody>();
        }

        public void Stop() {
            if (rb == null) return;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }


        public void PickUp() {
            isGrabbed = true;
            lifetime = 0;
            if (dispenser != null) dispenser.OnItemGrabbed(this);
        }

        public void Drop() {
            isGrabbed = false;
            ResetLifetime();
            if (dispenser != null) dispenser.OnItemDropped(this);
        }

        public void DespawnItem() {
            if (!enabled) return;
            if (dispenser != null) dispenser.DespawnItem(this);
        }

        public void ResetLifetime() {
            if (dispenser != null) lifetime = dispenser.itemLifetime;
            else lifetime = 5;
        }

        [ContextMenu("Despawn ASAP")]
        public void DespawnASAP() {
            skipLifetime = true;
        }

        public IEnumerator FiveSecondRule() {
            while (lifetime > 0) {
                if (skipLifetime) {
                    lifetime = 0;
                    break;
                }
                yield return new WaitForSeconds(1f);
                lifetime--;
            }
            if (!isGrabbed && !DespawnProtection || skipLifetime) DespawnItem();
            skipLifetime = false;
        }
    }
}