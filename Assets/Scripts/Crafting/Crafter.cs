using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype 
{
    public class Crafter : MonoBehaviour
    {
        public RecipeList recipeList;
        public InventoryManager inventoryManager;

        // public void Start() 
        // {
        //     Craft(5);
        //     inventoryManager.PrintInventory();
        // }

        public Potion Craft(int potionIndex) 
        {
            Recipe chosenRecipe = new Recipe();
            foreach (Recipe recipe in recipeList.recipeList)
            {
                if (recipe.GetPotionIndex() == potionIndex)
                {
                    chosenRecipe = recipe;
                    break;
                }                
            }

            Dictionary<int, int> ingredients = new Dictionary<int, int>();
            foreach (int itemID in chosenRecipe.GetList())
            {
                if (ingredients.ContainsKey(itemID)) 
                {
                    ingredients[itemID] += 1;
                } else 
                {
                    ingredients.Add(itemID, 1);
                }
            }

            bool craftable = InventoryManager.instance.CheckIfItemsAreInInventory(ingredients);
            if (craftable) 
            {
                // aquí se instancia la poción con el PotionManager
                Debug.Log("Poción completada");

                foreach (int itemID in chosenRecipe.GetList()) 
                {
                    InventoryManager.instance.RemoveItem(itemID);
                }

                return null; // cambiar por la poción instanciada

            } else 
            {
                Debug.Log("Faltan ingredientes");
                return null;
            }
        }
    }

}
