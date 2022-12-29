using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype 
{
    public class PruebaMalla : MonoBehaviour
    {
        public ItemList itemList;

        // Start is called before the first frame update
        void Start()
        {
            MeshFilter yourMesh = gameObject.GetComponent<MeshFilter>();
            Mesh newMesh = itemList.itemList[1].mesh;
            yourMesh.sharedMesh = newMesh;

        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
