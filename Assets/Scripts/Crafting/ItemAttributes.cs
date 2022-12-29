using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype 
{
  [System.Serializable]
  public struct ItemAttributes
  {
    public int id;
    public string name;
    //public bool instantiated;
    public Mesh mesh;
    public Material material;
    
  }

}
