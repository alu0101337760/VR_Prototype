using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    [CreateAssetMenu(fileName = "newPotionList", menuName = "ScriptableObjects/PotionList", order = 1)]
    public class PotionList : ScriptableObject
    {
        [SerializeField]
        public List<Potion> potions;
    }
}