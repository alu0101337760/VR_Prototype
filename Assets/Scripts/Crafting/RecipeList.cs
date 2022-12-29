using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    [CreateAssetMenu(fileName = "newRecipeList", menuName = "ScriptableObjects/RecipeList", order = 1)]
    public class RecipeList : ScriptableObject
    {
        [SerializeField]
        public List<Recipe> recipeList;
    }
}