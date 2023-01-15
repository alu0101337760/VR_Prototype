using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    public class PotionManager : MonoBehaviour
    {
        public static PotionManager instance { get; private set; }
        public GameObject potionPrefab;
        public PotionComponentsList potionComponents;

        private static Queue<Potion> usedPotionsQueue;

        private Potion currentPotion;

        private void Awake()
        {
            if (PotionManager.instance == null)
            {
                PotionManager.instance = this;
                usedPotionsQueue = new Queue<Potion>();
            }
        }

        public void AddUsedPotion(Potion potion)
        {
            usedPotionsQueue.Enqueue(potion);
        }

        private GameObject CreateNewPotion(int potionID, Vector3 pos, Quaternion rot)
        {
            GameObject newPotion = Instantiate(potionPrefab, pos, rot);
            PotionComponents potionComponent = potionComponents.potionComponents[potionID];

            newPotion.AddComponent(potionComponent.potionType);

            // if (potionComponent.potionFlask != null)
            // {
            //     newPotion.GetComponent<MeshFilter>().mesh = potionComponent.potionFlask;
            // }
            
            newPotion.transform.Find("Potion").GetComponent<MeshRenderer>().material = potionComponent.potionMaterial;

            return newPotion;
        }

        public GameObject InstantiatePotion(int potionID, Vector3 pos, Quaternion rot)
        {
            return CreateNewPotion(potionID, pos, rot);
        }

        public GameObject InstantiatePotion(PotionNames potionID, Vector3 pos, Quaternion rot)
        {
            return CreateNewPotion((int)potionID, pos, rot);
        }

        void Update()
        {
            for (int i = 0; i < usedPotionsQueue.Count; i++)
            {
                currentPotion = usedPotionsQueue.Peek();
                if (Time.time - currentPotion.timeOfActivation < currentPotion.effectDuration)
                {
                    currentPotion.StopPotionEffect();
                    break;
                }
                usedPotionsQueue.Dequeue();
                Destroy(currentPotion);
            }
        }
    }
}