using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace VR_Prototype 
{
    public class Crafter : MonoBehaviour
    {
        public RecipeList recipeList;
        public InventoryManager inventoryManager;

        private int numberOfItemsInCauldron;
        private List<int> cauldronContent;
        private List<GameObject> itemsInCauldron;

        public void Start() 
        {
            numberOfItemsInCauldron = 0;
            cauldronContent = new List<int>();
            itemsInCauldron = new List<GameObject>();


            // Craft(5);
            // inventoryManager.PrintInventory();
        }

        void OnTriggerEnter(Collider collided) 
        {
            if (collided.tag == "Item") {
                numberOfItemsInCauldron++;
                int id = collided.gameObject.GetComponent<poolItemID>().id;
                Debug.Log(collided.gameObject.name);
                Debug.Log("Inserted item " + id);
                cauldronContent.Add(id);
                itemsInCauldron.Add(collided.gameObject);
                
                if (numberOfItemsInCauldron == 4) {
                    int potion = -1;
                    foreach (Recipe recipe in recipeList.recipeList) {
                        if (cauldronContent.All(recipe.ingredients.Contains) && cauldronContent.Count == recipe.ingredients.Count) {
                            potion = recipe.potion;
                            break;
                        }
                    }
                    Debug.Log("Crafted potion " + potion);
                    ResetCauldron();
                }
            }
        }

        private void ResetCauldron() 
        {
            numberOfItemsInCauldron = 0;
            cauldronContent.Clear();
            foreach (GameObject item in itemsInCauldron) {
                ItemManager.instance.RemoveItem(item);
            }
        }


        // public Potion Craft(int potionIndex) 
        // {
        //     Recipe chosenRecipe = new Recipe();
        //     foreach (Recipe recipe in recipeList.recipeList)
        //     {
        //         if (recipe.GetPotionIndex() == potionIndex)
        //         {
        //             chosenRecipe = recipe;
        //             break;
        //         }                
        //     }

        //     Dictionary<int, int> ingredients = new Dictionary<int, int>();
        //     foreach (int itemID in chosenRecipe.GetList())
        //     {
        //         if (ingredients.ContainsKey(itemID)) 
        //         {
        //             ingredients[itemID] += 1;
        //         } else 
        //         {
        //             ingredients.Add(itemID, 1);
        //         }
        //     }

        //     bool craftable = InventoryManager.instance.CheckIfItemsAreInInventory(ingredients);
        //     if (craftable) 
        //     {
        //         // aquí se instancia la poción con el PotionManager
        //         Debug.Log("Poción completada");

        //         foreach (int itemID in chosenRecipe.GetList()) 
        //         {
        //             InventoryManager.instance.RemoveItem(itemID);
        //         }

        //         return null; // cambiar por la poción instanciada

        //     } else 
        //     {
        //         Debug.Log("Faltan ingredientes");
        //         return null;
        //     }
        // }
    }

}
