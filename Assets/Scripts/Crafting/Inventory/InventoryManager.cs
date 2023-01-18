using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VR_Prototype 
{
    public sealed class InventoryManager : MonoBehaviour
    {
        public UnityEvent<int> OnItemAdded;

        private Dictionary<int, int> inventory;
        private List<int> keys;

        // public static attribute that contains the only instance of InventoryManager
        public static InventoryManager instance;

        // empty constructor
        private InventoryManager() 
        {
            inventory = new Dictionary<int, int>();
        }

        private void Awake() 
        {
            if (instance == null) 
            {
                InventoryManager.instance = this;
            }
        }

        public int ItemQuantity(int itemID) {
            if (!keys.Contains(itemID)) return 0;
            return inventory[itemID];
        }

        public int RandomItem() {
            return keys[Random.Range(0, keys.Count)];
        }

        [ContextMenu("Add Items")]
        public void AddItems() {
            foreach (int i in new int[0, 1, 2, 3]) {
                AddItem(i);
            };
        }

        [ContextMenu("Remove Items")]
        public void RemoveItems() {
            foreach (int i in new int[0, 1, 2, 3]) {
                RemoveItem(i);
            };
        }

        public void AddKey(int key) 
        {
            if(!keys.Contains(key)) {
                keys.Add(key);
                inventory.Add(key, 0);
            }
        }

        public bool ContainsKey(int key) 
        {
            return keys.Contains(key);
        }
        public void AddItem(int newItem) 
        {
            if (keys.Contains(newItem)) 
            {
                inventory[newItem] += 1;
            } else 
            {
                keys.Add(newItem);
                inventory.Add(newItem, 1);
            }
            OnItemAdded.Invoke(newItem);
        }

        public void RemoveItem(int deletedItem = 0) 
        {
            if (keys.Contains(deletedItem) && inventory[deletedItem] > 0) 
            {
                inventory[deletedItem] -= 1;
            }
        }

        public bool CheckIfItemsAreInInventory(Dictionary<int, int> requestedItems)
        {
            foreach(KeyValuePair<int, int> itemQuantity in requestedItems)
            {
                if (keys.Contains(itemQuantity.Key)) 
                {
                    if (inventory[itemQuantity.Key] < requestedItems[itemQuantity.Key]) 
                    {
                        return false;
                    }
                } else 
                {
                    return false;
                }
            }

            return true;
        }

        [ContextMenu("PrintInventory")]
        public void PrintInventory() 
        {
            Debug.Log("Printing inventory");
            foreach (KeyValuePair<int, int> item in inventory)
            {
                Debug.Log($"Obj = {item.Key}, Cant = {item.Value}");
            }
        }
    }

}
