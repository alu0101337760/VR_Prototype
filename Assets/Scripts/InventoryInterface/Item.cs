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
        [HideInInspector]
        public bool isGrabbed = false;
        private int lifetime;
        private bool skipLifetime = false;

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
            if (!isGrabbed) DespawnItem();
            skipLifetime = false;
        }
    }
}