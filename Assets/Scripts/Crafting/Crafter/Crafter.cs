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
                    PotionManager.instance.InstantiatePotion(potion, (spawnPoint == null? transform.position + Vector3.up: spawnPoint.position), Quaternion.identity);
                    ResetCauldron();
                }
            }
        }

        private void ResetCauldron() 
        {
            numberOfItemsInCauldron = 0;
            cauldronContent.Clear();
        }
    }

}
