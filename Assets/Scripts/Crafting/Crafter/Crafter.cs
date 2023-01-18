using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace VR_Prototype 
{
    public class Crafter : MonoBehaviour
    {
        public RecipeList recipeList;
        public Transform spawnPoint;

        [SerializeField]
        private int numberOfItemsInCauldron;
        [SerializeField]
        private List<int> cauldronContent;

        public void Start() 
        {
            numberOfItemsInCauldron = 0;
            cauldronContent = new List<int>();


            // Craft(5);
            // inventoryManager.PrintInventory();
        }

        void OnTriggerEnter(Collider collided) 
        {
            if (collided.tag == "Item") {
                numberOfItemsInCauldron++;
                poolItem item = collided.gameObject.GetComponent<poolItem>();
                int id = item.id;
                cauldronContent.Add(id);
                item.DespawnItem();
                InventoryManager.instance.RemoveItem(id);

                if (numberOfItemsInCauldron == 4) {
                    int potion = -1;
                    foreach (Recipe recipe in recipeList.recipeList) {
                        if (cauldronContent.All(recipe.ingredients.Contains) && cauldronContent.Count == recipe.ingredients.Count) {
                            potion = recipe.potion;
                            break;
                        }
                    }
                    Debug.Log("Potion: " + potion);
                    PotionManager.instance.InstantiatePotion(0, (spawnPoint == null? transform.position + Vector3.up: spawnPoint.position), Quaternion.identity);
                    ResetCauldron();
                }
            }
        }

        private void ResetCauldron() 
        {
            numberOfItemsInCauldron = 0;
            cauldronContent.Clear();
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
