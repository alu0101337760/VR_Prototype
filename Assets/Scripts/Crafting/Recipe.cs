using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype 
{
  [System.Serializable]
  public class Recipe
  {
    public List<ItemAttributes> input;
    public string potionName;
    public int potion;

    public int GetPotionIndex() 
    {
      return potion;
    }

    public List<ItemAttributes> GetList() 
    {
      return input;
    }

  }
}
