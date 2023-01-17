using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype {
    public class ItemKicker : MonoBehaviour
    {
        public ItemDispenser dispenser;
        public float kickTime = 3f;
        private float kickTimer = 0f;

        void Update() {
            kickTimer += Time.deltaTime;
            if (kickTimer >= kickTime) {
                kickTimer = 0f;
                if (dispenser != null) 
                {
                    Item item = dispenser.GetItemInDropSpace();
                    if (item != null) {
                        Debug.Log("Kicking item");
                        item.GetComponent<Rigidbody>().AddForce(Vector3.right * 10f, ForceMode.Impulse);
                        item.PickUp();
                        item.Drop();
                    }
                }
            }
        }
    }
}