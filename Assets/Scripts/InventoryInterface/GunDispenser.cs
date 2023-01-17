using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace VR_Prototype {
    public class GunDispenser : ItemDispenser
    {
        public float spawnDelay = 5f;
        public override void OnItemGrabbed(Item item)
        {
            if (ItemInDropSpace(item)) {
                for (int i = 0; i < itemInstances.Count; i++) {
                    if (itemInstances[i] != item) DespawnItem(itemInstances[i]);
                }
            }
        }

        public override void OnItemDropped(Item item)
        {
            PistolBehaviour pistol = item.GetComponent<PistolBehaviour>();
            if (pistol != null && pistol.alreadyShot) DespawnItem(item);
            else StartCoroutine(item.FiveSecondRule());
        }

        protected override void CheckDropSpace()
        {
            if (ActiveItems() == 0 && EmptyDropSpace()) StartCoroutine(GunSpawn());
        }

        IEnumerator GunSpawn() {
            yield return new WaitForSeconds(spawnDelay);
            SpawnItem();
        }
    }
}