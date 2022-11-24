using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    public class PotionManager : MonoBehaviour
    {
        
        public static PotionManager instance { get; private set; }
        public static Queue<Potion> usedPotionsQueue;

        private static PotionComponentsList potionComponents;

        private Potion currentPotion;

        private void Start()
        {
            if (PotionManager.instance == null)
            {
                PotionManager.instance = this;
            }
        }


        public void AddUsedPotion(Potion potion)
        {
            usedPotionsQueue.Enqueue(potion);
        }


        void Update()
        {

            for (int i = 0; i < usedPotionsQueue.Count; i++)
            {
                currentPotion = usedPotionsQueue.Peek();
                if (Time.time - currentPotion.timeOfActivation < currentPotion.effectDuration)
                {
                    break;
                }
                usedPotionsQueue.Dequeue();
                Destroy(currentPotion);
            }
        }
    }
}