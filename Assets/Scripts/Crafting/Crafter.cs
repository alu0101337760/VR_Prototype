using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype 
{
    public class Crafter
    {
        private RecipeList recipeList;
        private InventoryManager inventoryManager;

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
            foreach (ItemAttributes item in chosenRecipe.GetList())
            {
                int itemID = item.id;
                if (ingredients.ContainsKey(itemID)) 
                {
                    ingredients[itemID] += 1;
                } else 
                {
                    ingredients.Add(itemID, 1);
                }
            }

            bool craftable = inventoryManager.CheckIfItemsAreInInventory(ingredients);
            if (craftable) 
            {
                // aquí se instancia la poción con el PotionManager

                foreach (ItemAttributes item in chosenRecipe.GetList()) 
                {
                    inventoryManager.RemoveItem(item.id);
                }

                return null; // cambiar por la poción instanciada

            } else 
            {
                return null;
            }
        }
    }

}
