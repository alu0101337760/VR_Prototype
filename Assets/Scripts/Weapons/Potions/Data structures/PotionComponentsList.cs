using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    [CreateAssetMenu(fileName = "newPotionComponentsList", menuName = "ScriptableObjects/PotionComponentsList", order = 1)]
    public class PotionComponentsList : ScriptableObject
    {
        [SerializeField]
        public List<PotionComponents> potionComponents;
    }
}