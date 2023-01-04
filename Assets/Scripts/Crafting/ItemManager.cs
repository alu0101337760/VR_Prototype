using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype 
{
  public sealed class ItemManager : MonoBehaviour
  {
    public ItemList itemList;
    public int poolSize;
    public int maxInstantiatedItems;
    public int instantiatedItems;
    public List<GameObject> objectList;

    // private static attribute that contains the only instance of ItemManager
    public static ItemManager instance;
    
    // empty constructor
    private ItemManager() 
    {
      poolSize = 300;
      maxInstantiatedItems = 10;
      instantiatedItems = 0;
    }

    private void Awake() 
    {
      if (instance == null) 
      {
        ItemManager.instance = this;
      }
    }

    public void InstantiateItem(int requiredId, Vector3 newPosition)
    {
      int requiredIndex = itemList.itemList.FindIndex(x => x.id.Equals(requiredId));
      if (requiredIndex != -1) {
        foreach (GameObject gameObject in objectList) {
          if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
            MeshFilter objectMesh = gameObject.GetComponent<MeshFilter>();
            Mesh newMesh = itemList.itemList[requiredIndex].mesh;
            objectMesh.sharedMesh = newMesh;
            Transform tf = gameObject.GetComponent<Transform>();
            tf.position = newPosition;

            break;
          }
        }
      }
    }

    public void RemoveItem(GameObject gameObject)
    {
      gameObject.SetActive(false);
      // MOVER A LA ZONA DONDE ESTÁN LOS OBJETOS NO INSTANCIADOS
      // En principio no debería hacer faltar quitar la malla, porque no se va a ver
    }

  }

}

