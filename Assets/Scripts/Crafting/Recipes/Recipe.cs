using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype 
{
  [System.Serializable]
  public class Recipe
  {
    public List<int> ingredients;
    public string potionName;
    public int potion;

    public int GetPotionIndex() 
    {
      return potion;
    }

    public List<int> GetList() 
    {
      return ingredients;
    }

  }
}
