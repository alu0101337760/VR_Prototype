using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype 
{
    public sealed class InventoryManager : MonoBehaviour
    {
        private Dictionary<int, int> inventory;

        // public static attribute that contains the only instance of InventoryManager
        public static InventoryManager instance;

        // empty constructor
        private InventoryManager() 
        {
            inventory = new Dictionary<int, int>();

            // ESTO ES PARA HACER PRUEBAS CON EL CRAFTEO. BORRAR
            // AddItem(0);
            // AddItem(1);
            // AddItem(1);
            // AddItem(1);
            // AddItem(2);
            // AddItem(3);
            // AddItem(3);
            // AddItem(3);
            // AddItem(3);

            // PrintInventory();
        }

        private void Awake() 
        {
            if (instance == null) 
            {
                InventoryManager.instance = this;
            }
        }

        public void AddItem(int newItem) 
        {
            if (inventory.ContainsKey(newItem)) 
            {
                inventory[newItem] += 1;
            } else 
            {
                inventory.Add(newItem, 1);
            }
        }

        public void RemoveItem(int deletedItem) 
        {
            if ((inventory[deletedItem] > 1) && (inventory.ContainsKey(deletedItem))) 
            {
                inventory[deletedItem] -= 1;
            }
        }

        public bool CheckIfItemsAreInInventory(Dictionary<int, int> requestedItems)
        {
            foreach(KeyValuePair<int, int> itemQuantity in requestedItems)
            {
                if (inventory.ContainsKey(itemQuantity.Key)) 
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
