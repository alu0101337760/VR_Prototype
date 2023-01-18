using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace VR_Prototype {
    public class InventoryDispenser : ItemDispenser
    {
        [SerializeField]
        private int itemType = 0;

        protected override void Start() {
            base.Start();
            InventoryManager.instance.AddKey(itemType);
            InventoryManager.instance.OnItemAdded.AddListener(StockReplenish);
        }

        public override void OnItemGrabbed(Item item)
        {
            if (ItemInDropSpace(item)) {
                for (int i = 0; i < itemInstances.Count; i++) {
                    if (itemInstances[i] != item) DespawnItem(itemInstances[i]);
                }
            }
        }
        protected override Item CreateItem() {
            Item item = base.CreateItem();
            item.GetComponent<poolItem>().id = itemType;
            return item;
        }
        public override void OnItemDropped(Item item)
        {
            StartCoroutine(item.FiveSecondRule());
        }

        protected override void CheckDropSpace()
        {
            if (InventoryManager.instance.ItemQuantity(itemType) > ActiveItems() && EmptyDropSpace()) SpawnItem();
        }

        private void StockReplenish(int type) {
            if (type == itemType) {
                CheckDropSpace();
            }
        }
    }
}