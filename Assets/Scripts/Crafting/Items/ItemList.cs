using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    [CreateAssetMenu(fileName = "newItemList", menuName = "ScriptableObjects/ItemList", order = 1)]
    public class ItemList : ScriptableObject
    {
        [SerializeField]
        public List<ItemAttributes> itemList;

    }
}