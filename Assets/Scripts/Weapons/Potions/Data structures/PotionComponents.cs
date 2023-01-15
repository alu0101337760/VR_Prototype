using System;
using UnityEngine;

namespace VR_Prototype
{
    [System.Serializable]
    public struct PotionComponents
    {
        public Material potionMaterial;
        public PotionNames potionName;
        public Mesh potionFlask;
        [SerializeField]
        public System.Type potionType;

        public PotionComponents(PotionComponents other, Type potionType) : this()
        {
            this.potionMaterial = other.potionMaterial;
            this.potionName = (PotionNames)Enum.Parse(typeof(PotionNames), potionType.ToString());
            this.potionFlask = other.potionFlask;
            this.potionType = potionType;
        }

        public PotionComponents(PotionNames name, Type potionType) : this()
        {
            this.potionName = name;
            this.potionType = potionType;
        }
    }
}