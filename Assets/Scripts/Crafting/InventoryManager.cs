using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hacerla singleton
public class InventoryManager : MonoBehaviour
{
    private Dictionary<int, int> inventory;

    public void AddItem(int newItem) 
    {
        if (!inventory.ContainsKey(newItem)) 
        {
            inventory[newItem] += 1;
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
}
