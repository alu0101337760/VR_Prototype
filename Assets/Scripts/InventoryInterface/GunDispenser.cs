using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace VR_Prototype {
    public class GunDispenser : ItemDispenser
    {
        public int allowedGuns = 2;
        public int gunCount = 0;
        public float spawnDelay = 5f;
        public override void OnItemGrabbed(Item item)
        {
        }

        public override void DespawnItem(Item item)
        {
            base.DespawnItem(item);
            gunCount--;
        }

        public override void OnItemDropped(Item item)
        {
            PistolBehaviour pistol = item.GetComponent<PistolBehaviour>();
            if (pistol != null && pistol.alreadyShot) DespawnItem(item);
            else StartCoroutine(item.FiveSecondRule());
        }

        protected override void CheckDropSpace()
        {
            if (gunCount < allowedGuns) StartCoroutine(GunSpawn());
        }

        IEnumerator GunSpawn() {
            gunCount++;
            yield return new WaitForSeconds(spawnDelay);
            while (!EmptyDropSpace()) yield return new WaitForSeconds(0.5f);
            SpawnItem();
        }
    }
}